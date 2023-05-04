using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        #region Properties 
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }
        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            protected set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }
        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }
        public int Level
        {
            get { return _level; }
            protected set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public bool IsDead => CurrentHitPoints <= 0;
        public event EventHandler OnKilled;

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        #endregion

        /// <summary>
        /// Constructor for LivingEntity
        /// </summary>
        protected LivingEntity(string name, int maximumHitPoints, 
            int currentHitPoints, int gold, int level=1)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;

            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }
        sdfjlkdsfklasfdlksdf

        public void TakeDamage(int amount)
        {
            CurrentHitPoints -= amount;
            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }
        public void Heal(int amount)
        {
            CurrentHitPoints += amount;
            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }
        public void HealCompletely()
        {
            CurrentHitPoints = MaximumHitPoints;
        }
        public void HealToPercent(int percent)
        {
            if (percent < 0 || percent > 100)
            {
                throw new ArgumentOutOfRangeException($"Cannot heal past 100%! Must enter value" +
                    $"between 0 and 1. Value provided: {percent}");
            }
            CurrentHitPoints = (int)Math.Round(MaximumHitPoints * percent / 100.0);
        }
        public void RecieveGold(int amount)
        {
            Gold += amount;
        }
        public void SpendGold(int amount)
        {
            if (amount > Gold)
            {
                throw new ArgumentOutOfRangeException(
                    $"{Name} only has {Gold} gold, which is insufficient for this purchase.");
            }
            Gold -= amount;
        }

        public void AddItemToInventory(GameItem itemToAdd)
        {
            Inventory.Add(itemToAdd);
            
            // If item is unique, add one to inventory.
            // If item is NOT unique:
            //      If the item is NOT in inventory, add with Qty 0
            //      Increment the quantity by 1
            if (itemToAdd.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(itemToAdd, 1));
            } 
            else 
            {
                if (!GroupedInventory.Any(i => i.Item.Id == itemToAdd.Id))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(itemToAdd, 0));
                }
                GroupedInventory.First(i => i.Item.Id == itemToAdd.Id).Quantity++;
            }
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem itemToRemove)
        {
            Inventory.Remove(itemToRemove);

            // If grouped inventory contains the item
            //      If the item has qty 1, remove it
            //      Else, decrement the qty by -1
            GroupedInventoryItem groupedItemToRemove = itemToRemove.IsUnique ?
                GroupedInventory.FirstOrDefault(i => i.Item == itemToRemove) :
                GroupedInventory.FirstOrDefault(i => i.Item.Id == itemToRemove.Id);

            if (groupedItemToRemove != null)
            {
                if (groupedItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedItemToRemove);
                }
                else
                {
                    groupedItemToRemove.Quantity--;
                }
            }
            OnPropertyChanged(nameof(Weapons));
        }

        #region Private Functions
        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }
        #endregion
    }
}
