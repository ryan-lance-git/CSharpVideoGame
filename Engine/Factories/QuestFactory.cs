﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            // Items needed to complete quest and rewarded items
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            // Clear herb garden quest
            _quests.Add(new Quest(1,
                                  "Clear the herb garden",
                                  "Defeat the snakes in the Herbalists Garden",
                                  itemsToComplete,
                                  25, 10, 
                                  rewardItems));

            // Clear the farmer's field quest
            _quests.Add(new Quest(2,
                                  "Clear the herb garden",
                                  "Defeat the snakes in the Herbalists Garden",
                                  itemsToComplete,
                                  25, 10,
                                  rewardItems));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
