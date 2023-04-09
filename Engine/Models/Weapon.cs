using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MaximumDamage { get; set; }
        public int MinimumDamage { get; set; }
        public Weapon(int itemTypeID, string name, int price, 
            int minDamage, int maxDamage, string description) 
            : base(itemTypeID, name, price, description, true)
        {
            MaximumDamage = maxDamage;
            MinimumDamage = minDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(Id, Name, Price, MinimumDamage, MaximumDamage, Description);
        }
    }
}
