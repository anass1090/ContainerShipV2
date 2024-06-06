using ContainerShipV2.Exceptions;
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

        private void ResetData()
        {
            TotalWeight = 0;
            WeightLeft = 0;
            WeightRight = 0;
            WeightCenter = 0;
            WeightDifference = 0;
            RowList = GetAllRowsInShip();
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

        public void Run()
        {
            TotalSlots = Length * Width * 31;
            int TotalWeightFirstRow = Width * 150;
            int totalWeightCoolables = 0;
            foreach (Container container in containers)
            {
                if ((int)container.containerType == 3 || (int)container.containerType == 4)
                {
                    totalWeightCoolables+= container.Weight;
                }
            }
            if (TotalWeightFirstRow < totalWeightCoolables)
            {
                throw new ShipException("Too many coolables!!!!");
            }

            if (containers.Count > TotalSlots)
            {
                throw new ShipException("Ship is too small");
            }

            if (GetTotalWeight() > MaxWeight)
            {
                throw new ShipException("Load is too heavy");
            }

            if (DistributeContainers())
            {
                if (WeightDifference < 20)
                {
                    StartVisualizer();
                }
                else
                {

                    throw new ShipException("Ship is capsizing");
                }
            }

        }


        public bool DistributeContainers()
        {
            ResetData();
            int TotalWeight = GetTotalWeight();

            if (TotalWeight >= MinWeight)
            {

                sortedContainers = containers.OrderByDescending(o => o.containerType).ThenBy(o => o.Weight).ToList();


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
            else
            {
                throw new ContainerException("Containers are too light");
            }

            return true;
        }


        private bool AddContainerLeftOrRight(Container container, int index)
        {


            for (int i = 0; i < RowList.Count; i++)
            {

                if (WeightLeft < WeightRight)
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

            if (Width % 2 == 0) //Is even
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

        private void StartVisualizer()
        {
            string stack = "";
            string weight = "";
            for (int z = 0; z < RowList.Count; z++)
            {
                if (z > 0)
                {
                    stack += '/';
                    weight += '/';
                }

                for (int x = 0; x < RowList[z].stackList.Count; x++)
                {
                    if (x > 0)
                    {
                        stack += ",";
                        weight += ",";
                    }

                    if (RowList[z].stackList[x].containers.Count > 0)
                    {
                        for (int y = 0; y < RowList[z].stackList[x].containers.Count; y++)
                        {
                            Container container = RowList[z].stackList[x].containers[y];

                            stack += Convert.ToString((int)container.containerType);
                            weight += Convert.ToString(container.Weight);
                            if (y < (RowList[z].stackList[x].containers.Count - 1))
                            {
                                weight += "-";
                            }
                        }
                    }
                }
            }

            Process.Start($"https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length=" + Length + "&width=" + Width + "&stacks=" + stack + "&weights=" + weight + "");
        }
    }
}
