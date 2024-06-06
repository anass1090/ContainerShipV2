using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerShipV2
{
    public class Container
    {
        public int Weight { get; private set; } 
        private int MaxWeight = 30;
        public int MinWeight { get; private set; } = 4;

        public bool Coolable { get; private set; }
        public bool Valuable { get; private set; }

        public ContainerTypes containerType { get; private set; }


        public Container(int weight, int type) 
        {
            Weight = SetWeight(weight);
            containerType = (ContainerTypes)type;
            Valuable = SetTypeValuable(containerType);
            Coolable = SetTypeCoolable(containerType);
        }

        public enum ContainerTypes
        {
            Normal = 1,
            Valuable = 2,
            Coolable = 3,
            CoolableValuable = 4
        }

        private bool SetTypeValuable(ContainerTypes type)
        {
            if (type == ContainerTypes.Valuable || type == ContainerTypes.CoolableValuable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool SetTypeCoolable(ContainerTypes type)
        {
            if (type == ContainerTypes.Coolable || type == ContainerTypes.CoolableValuable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private int SetWeight(int weight)
        {
            if (weight < MinWeight)
            {
                throw new Exception("Weight is mininum is 4 tons");
            }
            else if (weight > MaxWeight)
            {
                throw new Exception("Weight maximun is 30 tons");
            }

            return weight;
        }
    }
}
