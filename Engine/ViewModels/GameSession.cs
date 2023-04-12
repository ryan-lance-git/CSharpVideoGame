using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties
        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if(_currentPlayer != null)
                {
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
            }
        }
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }
                _currentMonster = value;
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                }

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }
            }
        }

        public Weapon CurrentWeapon { get; set; }
        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public bool HasLocationToNorth => CurrentWorld
            .LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate+1) != null;
        public bool HasLocationToSouth => CurrentWorld
            .LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate-1) != null;
        public bool HasLocationToEast => CurrentWorld
            .LocationAt(CurrentLocation.XCoordinate+1, CurrentLocation.YCoordinate) != null;
        public bool HasLocationToWest => CurrentWorld
            .LocationAt(CurrentLocation.XCoordinate-1, CurrentLocation.YCoordinate) != null;
        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;
        
        #endregion Properties

        #region Movement
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        #endregion

        public GameSession() 
        {
            CurrentPlayer = new Player("Ryan","Fighter",15,20,25,0,1);

            GameItem startingItem = ItemFactory.CreateGameItem(1001);

            CurrentWeapon = (Weapon)startingItem;
            CurrentPlayer.AddItemToInventory(startingItem);

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                // Check if the quest at current location is in the list of current player's quests
                QuestStatus questToComplete = 
                    CurrentPlayer.Quests.FirstOrDefault(
                        q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                // If a quest exists and the player has all the necessary items to complete, then complete the quest
                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(
                                    CurrentPlayer.Inventory.First(item => item.Id == itemQuantity.ItemID));
                            }
                        }
                        RaiseMessage($"\nYou completed the '{quest.Name}' quest.");

                        // Disburse the gold, experience and reward items for completing the quest
                        CurrentPlayer.ReceiveExperience(quest.RewardExperiencePoints);
                        CurrentPlayer.RecieveGold(quest.RewardGold);
                        RaiseMessage($"You recieve {quest.RewardExperiencePoints} experience points.");
                        RaiseMessage($"You recieve {quest.RewardGold} gold.");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You also recieve a {rewardItem.Name}");
                        }

                        // Mark the quest complete
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage($"\nYou recieve the '{quest.Name}' quest.");
                    RaiseMessage(quest.Description);
                    RaiseMessage($"You must return with the following items to complete the quest:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"     {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    RaiseMessage($"The rewards for completing the quest are:");
                    RaiseMessage($"     {quest.RewardExperiencePoints} Experience");
                    RaiseMessage($"     {quest.RewardGold} Gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"     {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }
        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }
        public void AttackCurrentMonster()
        {
            // Make sure the player has a weapon
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon to attack with first!");
                return;
            }

            // Determine damage to monster
            int damageToMonster = RandomNumberGenerator.SimpleNumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage("You missed!");
                return;
            }
            else
            {
                CurrentMonster.TakeDamage(damageToMonster);
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} damage.");
            }

            // If the player kills the monster
            if (CurrentMonster.IsDead)
            {
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // If the monster is still alive, let the monster attack
                int damageToPlayer = RandomNumberGenerator.SimpleNumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);
                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacked and missed!");
                }
                else
                {
                    RaiseMessage($"The {CurrentMonster.Name} hit the player for {damageToPlayer} damange.");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }
        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"The {CurrentMonster.Name} killed you. ");
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.HealToPercent(80);
        }
        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"\n\rYou defeated the {CurrentMonster.Name}! You recieved " +
                $"{CurrentMonster.RewardExperiencePoints} experience points and " +
                $"{CurrentMonster.Gold} reward gold.");

            CurrentPlayer.ReceiveExperience(CurrentMonster.RewardExperiencePoints);
            CurrentPlayer.RecieveGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                CurrentPlayer.AddItemToInventory(gameItem);
                RaiseMessage($"You recieved a {gameItem.Name}");
            }
        }
        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

    }
}
