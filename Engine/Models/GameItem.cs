using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Misc,
            Weapon
        }
        public ItemCategory Category { get; }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }
        public int HealAmount { get; }
        private string _smallImageName { get; }
        public string ImageName { get; }
        public GameItem(ItemCategory category, int itemTypeID, string name, string imageName, int price, 
            string description, bool isUnique=false, int minimumDamage = 0, int maximumDamage = 0, 
            int healAmount = 0)
        {
            Category = category;
            Id = itemTypeID;
            Name = name;
            Description = description;
            Price = price;
            IsUnique = isUnique;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            HealAmount = healAmount;

            _smallImageName = imageName;
            ImageName = $"/Engine;component/Images/Items/{imageName}";
        }

        public GameItem Clone()
        {
            return new GameItem(Category, Id, Name, _smallImageName, Price, 
                Description, IsUnique, MinimumDamage, MaximumDamage, HealAmount);
        }
    }
}
