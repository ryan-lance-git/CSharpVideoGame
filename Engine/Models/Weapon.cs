using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MaximumDamage { get; }
        public int MinimumDamage { get; }
        public string _smallImageName { get; }
        public Weapon(int itemTypeID, string name, string imageName, int price, 
            int minDamage, int maxDamage, string description) 
            : base(itemTypeID, name, imageName, price, description, true)
        {
            MaximumDamage = maxDamage;
            MinimumDamage = minDamage;
            _smallImageName = imageName;
        }

        public new Weapon Clone()
        {
            return new Weapon(Id, Name, _smallImageName, Price, MinimumDamage, MaximumDamage, Description);
        }
    }
}
