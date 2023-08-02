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
        
        public string CharacterClass
        {
            get { return _characterClass; }
            private set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
               
        }
        public int ExperiencePoints 
        { 
            get { return _experiencePoints;  } 
            private set 
            { 
                _experiencePoints = value;
                OnPropertyChanged();

                SetLevelAndMaximumHitPoints();
            }
        }
        public ObservableCollection<QuestStatus> Quests { get; }

        public event EventHandler OnLeveledUp;

        public Player(string name, string characterClass, int currentHitPoints,
            int maximumHitPoints, int gold, int experiencePoints, int level)
            : base(name, maximumHitPoints, currentHitPoints, gold, level)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;

            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddExperience(int experienceAmount)
        {
            ExperiencePoints += experienceAmount;
        }

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
        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            int newLevel = Convert.ToInt32(Math.Floor(ExperiencePoints * 0.01)) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints = newLevel * 10;

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

    }
}
