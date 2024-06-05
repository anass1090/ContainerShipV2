using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShipV2
{
    public class Ship
    {

        List<Container> containers = new List<Container>();
        private List<Container> sortedContainers = new List<Container>();


        private List<Row> RowList = new List<Row>();
        public int Width { get; private set; }
        public int Length { get; private set; }
        public int MaxWeight { get; private set; }
        public int MinWeight { get; private set; }
        public int TotalSlots { get; private set; }

        private int WeightDifference;
        private int WeightLeft;
        private int WeightCenter;
        private int WeightRight;
        private int TotalWeight;



        public Ship(int lenght, int width)
        {
            Width = width;
            Length = lenght;
            MaxWeight = (lenght * width) * 150;

            MinWeight = MaxWeight / 2;
        }

        public void AddContainer(Container container)
        {
            containers.Add(container);
        }

        public int GetTotalWeight()
        {
            int totalWeight = 0;

            foreach (Container container in containers)
            {
                totalWeight += container.Weight;
            }
            
            return totalWeight;
        }

        public string Run()
        {
            TotalSlots = Length * Width;

            if (MaxWeight < TotalWeight)
            {
                return "Ship is to heavy";
            }

            if (containers.Count > TotalSlots)
            {
                return "Ship is too small";
            }

            if (GetTotalWeight() > MaxWeight)
            {
                return "Load is too heavy";
            }

            if ()
            {

            }

            return "Succes";
        }


        public bool DistributeContainers()
        {
            int TotalWeight = GetTotalWeight();

            if (TotalWeight >= MinWeight && TotalWeight <= MinWeight)
            {
                sortedContainers = containers.OrderByDescending(w => w.Weight).ToList();

                foreach (Container container in sortedContainers)
                {
                    if (AddContainerLeftOrRight(container))
                    {

                    }
                }
            }

            return true;
        }


        private bool AddContainerLeftOrRight(Container container)
        {
            foreach (Row row in RowList)
            {

            }
            return false;
        }

        private void SetLink()
        {
            //Process.Start();
        }
    }
}
