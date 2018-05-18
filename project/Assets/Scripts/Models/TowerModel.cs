using System.Collections.Generic;
using System.Xml.Serialization;

namespace Models
{
    [XmlType]
    public class RockModel : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
    
    [XmlType]
    public class TinnyTower : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
    
    [XmlType]
    public class SmallTower : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
    
    [XmlType]
    public class MediumTower : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
    
    [XmlType]
    public class LargeTower : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
    
    [XmlType]
    public class HugeTower : IItem
    {
        public float BuyPrice
        {
            get { return 0; }
        }
        
        public float SellPrice
        {
            get { return 0; }
        }

        public IEnumerable<IWeapon> Weapons
        {
            get { return null; }
        }

        public IEnumerable<IArmor> Armors
        {
            get { return null; }
        }
    }
}