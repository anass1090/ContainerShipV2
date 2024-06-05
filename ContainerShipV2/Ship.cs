using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private float WeightDifference;
        private int WeightLeft;
        private int WeightCenter;
        private int WeightRight;
        private int TotalWeight;



        public Ship(int lenght, int width)
        {
            Width = width;
            Length = lenght;
            MaxWeight = (lenght * width) * 150;
            RowList = GetAllRowsInShip();
            MinWeight = MaxWeight / 2;
        }

        public void AddContainer(Container container)
        {
            containers.Add(container);
        }

        public int GetTotalWeight()
        {
            TotalWeight = 0;

            foreach (Container container in containers)
            {
                TotalWeight += container.Weight;
            }
            
            return TotalWeight;
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

            if (DistributeContainers())
            {
                if (WeightDifference < 20)
                {
                    //start
                }
                else
                {
                    return "Ship is capsizing";
                }
            }

            return "Succes";
        }


        public bool DistributeContainers()
        {
            int TotalWeight = GetTotalWeight();

            if (TotalWeight >= MinWeight && TotalWeight <= MinWeight)
            {

                sortedContainers = containers.OrderByDescending(w => w.Weight).ToList();

                for (int i = 0; i < sortedContainers.Count; i++)
                {
                    if (AddContainerLeftOrRight(sortedContainers[i], i))
                    {
                        CheckNewShipWeightDifference();
                    }
                    else
                    {
                        AddContainerCentre(sortedContainers[i]);
                    }


                }


            }

            return true;
        }


        private bool AddContainerLeftOrRight(Container container, int index)
        {


            for (int i = 0; i < RowList.Count; i++)
            {
                if (WeightLeft <= WeightRight)
                {
                    if ((int)RowList[i].side == 1)
                    {
                        if (RowList[i].TryAddingContainer(container))
                        {
                            WeightLeft += container.Weight;
                            TotalWeight += container.Weight;
                            return true;
                        }
                    }

                }
                else if (WeightLeft >= WeightRight)
                {

                    if ((int)RowList[i].side == 3)
                    {

                        if (RowList[i].TryAddingContainer(container))
                        {
                            WeightRight += container.Weight;
                            TotalWeight += container.Weight;
                            return true;
                        }

                    }

                }

            }



            return false;
        }


        private bool AddContainerCentre(Container container)
        {
            for (int i = 0; i < RowList.Count; i++)
            {
                if ((int)RowList[i].side == 2)
                {
                    if (RowList[i].TryAddingContainer(container))
                    {
                        WeightCenter += container.Weight;
                        TotalWeight += container.Weight;
                        return true;
                    }

                }


            }

            return false;
        }


        private List<Row> GetAllRowsInShip()
        {
            List<Row> rows = new List<Row>();

            if (Width % 2 == 0) //Is Even
            {
                double middle = Width / 2;

                for (int i = 0; i < Width; i++)
                {
                    int side;
                    if (i < middle)
                    {
                        side = 1;
                    }
                    else
                    {
                        side = 3;
                    }

                    rows.Add(new Row(Length, side));
                }
            }
            else //Is niet even
            {
                double middle = Math.Floor(Width / 2f);
                for (int i = 0; i < Width; i++)
                {
                    int side = 2;
                    if (i < middle)
                    {
                        side = 1;
                    }
                    else if (i > middle)
                    {
                        side = 3;
                    }

                    rows.Add(new Row(Length, side));
                }
            }


            return rows;
        }

        private void CheckNewShipWeightDifference()
        {
            double L = ((double)WeightLeft / (double)TotalWeight) * 100;
            double R = ((double)WeightRight / (double)TotalWeight) * 100;

            if (Width % 2 == 0)
            {
                WeightDifference = (float)L - (float)R;
            }
            else
            {
                WeightDifference = (float)L - (float)R;
            }

        }

    }
}
