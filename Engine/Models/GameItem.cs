using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsUnique { get; set; }
        public GameItem(int itemTypeID, string name, int price, 
            string description, bool isUnique=false)
        {
            Id = itemTypeID;
            Name = name;
            Description = description;
            Price = price;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(Id, Name, Price, Description);
        }
    }
}
