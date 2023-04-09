using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        private string _characterClass;
        private int _experiencePoints;
        private int _level;
        
        public string CharacterClass
        {
            get { return _characterClass; }
            private set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
               
        }
        public int ExperiencePoints 
        { 
            get { return _experiencePoints;  } 
            private set 
            { 
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; set; }

        public Player(string name, string characterClass, int currentHitPoints,
            int maximumHitPoints, int gold, int experiencePoints, int level)
            : base(name, maximumHitPoints, currentHitPoints, gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Level = level;

            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void ReceiveExperience(int amount)
        {
            ExperiencePoints += amount;
        }

        /// <summary>
        /// Method used to complete quests.
        /// Checks if the List of Item Quantities is present in the inventory. 
        /// (i.e. does the player have 2 of Rat tail and 3 snake fang etc.)
        /// </summary>
        /// <param name="itemsNeeded"></param>
        /// <returns></returns>
        public bool HasAllTheseItems(List<ItemQuantity> itemsNeeded)
        {
            foreach (ItemQuantity itemQuantity in itemsNeeded)
            {
                if (Inventory.Count(i => i.Id == itemQuantity.ItemID) < itemQuantity.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
