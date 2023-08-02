using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        public string ImageName { get; }
        public Trader(string name, string imageName) : base (name, 9999, 9999, 9999)
        {
            ImageName = $"/Engine;component/Images/Traders/{imageName}";
        }
    }
}
