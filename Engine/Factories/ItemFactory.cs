using System;
using System.Collections.Generic;
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
            _standardGameItems.Add(new Weapon(1001, "Stick", "Stick.jpg", 0, 1, 2, 
                "Stick found on the ground. Maybe good for roasting marshmallows."));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword", "RustySword.jpg", 5, 1, 3, 
                "Effectively a blunt piece of iron. It needs to be sharpened."));
            _standardGameItems.Add(new Weapon(1003, "Wooden Club", "WoodenClub.jpg", 4, 1, 2, 
                "A heafty log with a tapered end."));

            // Enchantment
            _standardGameItems.Add(new GameItem(2001, "Small Red Jewel", "SmallRedJewel.jpg", 10,
                "A small red jewel that sparkles in the light."));
            _standardGameItems.Add(new GameItem(2002, "Small Green Jewel", "SmallGreenJewel.jpg", 15,
                "A small green jewel that sparkles in the light."));
            _standardGameItems.Add(new GameItem(2003, "Small Purple Jewel", "SmallPurpleJewel.jpg", 34,
                "A small purple jewel that sparkles in the light."));
            _standardGameItems.Add(new GameItem(2010, "Binding Wrap", "BindingWraps.jpg", 55,
                "An irridescent set of binds used for enchanting."));

            // Potions
            _standardGameItems.Add(new GameItem(3001, "Basic Healing Potion", "HealingPotion.jpg", 6,
                "A drinkable potion to restore health."));

            // Ingredients and Miscellaenous
            _standardGameItems.Add(new GameItem(9001, "Snake Fang", "SnakeFang.jpg", 2, 
                "Maybe could be used for small jewelry."));
            _standardGameItems.Add(new GameItem(9002, "Snakeskin", "SnakeSkip.jpg", 1, 
                "Wrikely snakeskin. Possibly an ingredient for something?"));
            _standardGameItems.Add(new GameItem(9003, "Rat tail", "RatTail.jpg", 1,
                "An ugly rat tail... gross!")); 
            _standardGameItems.Add(new GameItem(9004, "Rat fur", "RatFur.jpg", 2,
                "Not sure who will find this useful..."));
            _standardGameItems.Add(new GameItem(9005, "Spider fang", null, 1, 
                "Wee spider fang, doesn't look so intimidating when it's not attached to the live thing."));
            _standardGameItems.Add(new GameItem(9006, "Spider silk", null, 2,
                "Why are you holding this? It sticks everywhere!"));

        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standarditem = _standardGameItems.FirstOrDefault(
                item => item.Id == itemTypeID);

            if (standarditem != null)
            {
                // If the Item Type is a weapon, return a Weapon not a GameItem
                if (standarditem is Weapon)
                {
                    return (standarditem as Weapon).Clone();
                }

                return standarditem.Clone();
            }

            return null;
        }
    }
}
