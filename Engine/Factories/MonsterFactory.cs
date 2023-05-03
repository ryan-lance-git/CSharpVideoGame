using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Monster snake = new Monster("Snake", "Snake.jpg", 4, 4, 0, 5, 5, 0);
                    AddLootItem(snake, 9001, 75);
                    AddLootItem(snake, 9002, 25);
                    return snake;
                case 2:
                    Monster rat = new Monster("Rat", "Rat.jpg", 5, 5, 1, 3, 5, 0);
                    AddLootItem(rat, 9003, 75);
                    AddLootItem(rat, 9004, 25);
                    return rat;
                case 3:
                    Monster giantSpider = new Monster("Giant Spider", "GiantSpider.jpg", 10, 10, 5, 7, 10, 0);
                    AddLootItem(giantSpider, 9005, 75);
                    AddLootItem(giantSpider, 9006, 25);
                    return giantSpider;
                case 4:
                    Monster goblin = new Monster("Goblin", "Goblin.jpg", 20, 20, 3, 13, 25, 7);
                    AddLootItem(goblin, 2001, 50);
                    return goblin;
                default:
                    throw new ArgumentException(string.Format("Monster Type '{0}' does not exist.",
                        monsterID));
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int probabilityOfHaving)
        {
            if (RandomNumberGenerator.SimpleNumberBetween(1, 100) <= probabilityOfHaving)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }
    }
}
