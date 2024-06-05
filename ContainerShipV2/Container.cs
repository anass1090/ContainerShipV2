using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShipV2
{
    public class Container
    {
        public int Weight { get; private set; } 
        private int MaxWeight = 30;
        public int MinWeight { get; private set; } = 4;

        public bool Coolable { get; private set; }
        public bool Valuable { get; private set; }


        public Container(int weight, bool valuable, bool coolable) 
        {
            Weight = SetWeight(weight);
            Valuable = valuable;
            Coolable = coolable;
        }

        private int SetWeight(int weight)
        {
            if (Weight < MinWeight)
            {
                throw new Exception("Weight is mininum is 4 tons");
            }
            else if (weight > MaxWeight)
            {
                throw new Exception("Weight maximun is 30 tons");
            }

            return weight;
        }

        public string ReturnContainerInfoString()
        {
            return $"Weight: {Weight} Valuable: {Valuable} Coolable: {Coolable}"; ;
        }
    }
}
