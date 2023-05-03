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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsUnique { get; set; }
        private string _smallImageName { get; set; }
        public string ImageName { get; set; }
        public GameItem(int itemTypeID, string name, string imageName, int price, 
            string description, bool isUnique=false)
        {
            Id = itemTypeID;
            Name = name;
            Description = description;
            Price = price;
            IsUnique = isUnique;

            _smallImageName = imageName;
            ImageName = $"/Engine;component/Images/Items/{imageName}";

            if (imageName != null)
            {
                
                //ImageName = imageName;
            } 
            else 
            { 
                ImageName = null; 
            }
        }

        public GameItem Clone()
        {
            return new GameItem(Id, Name, _smallImageName, Price, Description);
        }
    }
}
