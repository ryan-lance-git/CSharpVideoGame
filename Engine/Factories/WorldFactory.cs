using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    static internal class WorldFactory
    {
        static internal World CreateWorld()
        {
            World newWorld = new World();

            newWorld.AddLocation(-2, -1, "Farmer's Field", 
                "The farmers field. Your allergies are kicking up!",
                "FarmFields.jpg");
            newWorld.LocationAt(-2, -1).AddMonster(2, 100); // Rats

            newWorld.AddLocation(-1, -1, "Farmer's House", 
                "This is the house of your neighbor, Farmer Buck.",
                "Farmhouse.jpg");
            newWorld.LocationAt(-1, -1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(2)); // Clear Farmer's field
            newWorld.LocationAt(-1, -1).TraderHere = TraderFactory.GetTraderByName("Farmer Ted");

            newWorld.AddLocation(0, -1, "Home", 
                "This is your home.",
                "Home.jpg");

            newWorld.AddLocation(-1, 0, "Trading Shop", 
                "The shop of Susan, the trader",
                "Trader.jpg");
            newWorld.LocationAt(-1, 0).TraderHere = TraderFactory.GetTraderByName("Susan");

            newWorld.AddLocation(0, 0, "Town Square", 
                "There is a fountain in the middle of the town square",
                "TownSquare.jpg");

            newWorld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "TownGate.jpg");

            newWorld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "SpiderForest.jpg");
            newWorld.LocationAt(2, 0).AddMonster(1, 5); // Rat
            newWorld.LocationAt(2, 0).AddMonster(2, 5); // Snake
            newWorld.LocationAt(2, 0).AddMonster(3, 90); // Spider

            newWorld.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "HerbalistsHut.jpg");
            newWorld.LocationAt(0, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1)); // Clear Herb Garden
            newWorld.LocationAt(0, 1).TraderHere = TraderFactory.GetTraderByName("Pete the Herbalist");

            newWorld.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "HerbalistsGarden.jpg");
            newWorld.LocationAt(0, 2).AddMonster(1, 100); // Snake

            return newWorld;
        }
    }
}
