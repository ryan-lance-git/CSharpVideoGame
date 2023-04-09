using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {
            Trader susan = new Trader("Susan");
            susan.AddItemToInventory(ItemFactory.CreateGameItem(1001)); // Stick
            susan.AddItemToInventory(ItemFactory.CreateGameItem(3001)); // Healing potion
            susan.AddItemToInventory(ItemFactory.CreateGameItem(9005)); // Spider fang

            Trader farmerTed = new Trader("Farmer Ted");
            farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(2001)); // Small red jewel
            farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(1003)); // Wooden club

            Trader peteTheHerbalist = new Trader("Pete the Herbalist");
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(3001)); // Healing potion
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(9003)); // Rat tail
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(9006)); // Spider silk
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(2010)); // Binding wrap

            _traders.Add(susan);
            _traders.Add(farmerTed);
            _traders.Add(peteTheHerbalist);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        public static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader by the name of '{trader.Name}'");
            }
            _traders.Add(trader);
        }
    }
}
