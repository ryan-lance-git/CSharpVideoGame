using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    /// <summary>
    /// ItemFactory 
    /// Contains a list of standard game items and methods to generate instances of those items.
    /// </summary>
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {
            // Weapons
            BuildWeapon(1001, "Stick", "Stick.jpg", 0, 1, 2, 
                "Stick found on the ground. Maybe good for roasting marshmallows.");
            BuildWeapon(1002, "Rusty Sword", "RustySword.jpg", 5, 1, 3, 
                "Effectively a blunt piece of iron. It needs to be sharpened.");
            BuildWeapon(1003, "Wooden Club", "WoodenClub.jpg", 4, 1, 2, 
                "A heafty log with a tapered end.");

            // Enchantment
            BuildMiscellaneousItem(2001, "Small Red Jewel", "SmallRedJewel.jpg", 10,
                "A small red jewel that sparkles in the light.");
            BuildMiscellaneousItem(2002, "Small Green Jewel", "SmallGreenJewel.jpg", 15,
                "A small green jewel that sparkles in the light.");
            BuildMiscellaneousItem(2003, "Small Purple Jewel", "SmallPurpleJewel.jpg", 34,
                "A small purple jewel that sparkles in the light.");
            BuildMiscellaneousItem(2010, "Binding Wrap", "BindingWraps.jpg", 55,
                "An irridescent set of binds used for enchanting.");

            // Potions
            BuildMiscellaneousItem(3001, "Basic Healing Potion", "HealingPotion.jpg", 6,
                "A drinkable potion to restore health.");

            // Ingredients and Miscellaenous
            BuildMiscellaneousItem(9001, "Snake Fang", "SnakeFang.jpg", 2, 
                "Maybe could be used for small jewelry.");
            BuildMiscellaneousItem(9002, "Snakeskin", "SnakeSkip.jpg", 1, 
                "Wrikely snakeskin. Possibly an ingredient for something?");
            BuildMiscellaneousItem(9003, "Rat tail", "RatTail.jpg", 1,
                "An ugly rat tail... gross!");
            BuildMiscellaneousItem(9004, "Rat fur", "RatFur.jpg", 2,
                "Not sure who will find this useful...");
            BuildMiscellaneousItem(9005, "Spider fang", null, 1, 
                "Wee spider fang, doesn't look so intimidating when it's not attached to the live thing.");
            BuildMiscellaneousItem(9006, "Spider silk", null, 2,
                "Why are you holding this? It sticks everywhere!");

        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(item => item.Id == itemTypeID)?.Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, string imageName, int price, string desciption)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Misc, 
                id, name, imageName, price, desciption));
        }
        private static void BuildWeapon(int id, string name, string imageName, int price, 
            int minimumDamage, int maximumDamage, string desciption)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Weapon, id, name, imageName, price, 
                desciption, true, minimumDamage, maximumDamage));
        }
    }
}
