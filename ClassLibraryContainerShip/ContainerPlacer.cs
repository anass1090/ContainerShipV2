using ClassLibraryContainerShip.Exceptions;
using System.Diagnostics;
using System;
using System.Linq;

namespace ClassLibraryContainerShip
{
    public class ContainerPlacer
    {
        public readonly Ship Ship;

        public ContainerPlacer(Ship ship)
        {
            Ship = ship;
        }

        public void Run()
        {
            if (Ship.TotalWeight < Ship.MinWeight)
            {
                throw new ContainerException("Containers are too light");
            }

            if (Ship.WeightDifferencePercentage > 20)
            {
                throw new ShipException("Ship is capsizing");
            }

            if (DistributeContainers())
            {
                StartVisualizer();
            }
        }

        private bool DistributeContainers()
        {
            Ship.Containers = Ship.Containers.OrderByDescending(o => o.ContainerType).ThenBy(o => o.Weight).ToList();

            foreach (Container container in Ship.Containers)
            {
                if (!AddContainerLeftOrRight(container))
                {
                    if (!AddContainerCenter(container).Item1)
                    {
                        Ship.UnfitContainers.Add(AddContainerCenter(container).Item2);
                    }
                }
            }

            return true;
        }

        private bool AddContainerLeftOrRight(Container container)
        {
            foreach (Row row in Ship.RowList)
            {
                if ((Ship.WeightLeft < Ship.WeightRight && row.Side == Row.Sides.Left) || (Ship.WeightLeft >= Ship.WeightRight && row.Side == Row.Sides.Right))
                {
                    if (row.TryAddingContainer(container))
                    {
                        if (Ship.WeightLeft < Ship.WeightRight)
                        {
                            Ship.WeightLeft += container.Weight;
                        }
                        else
                        {
                            Ship.WeightRight += container.Weight;
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        private (bool, Container) AddContainerCenter(Container container)
        {
            foreach (Row row in Ship.RowList)
            {
                if (row.Side == Row.Sides.Centre)
                {
                    if (row.TryAddingContainer(container))
                    {
                        return (true, container);
                    }
                }
            }
            return (false, container);
        }

        private string StartVisualizer()
        {
            string fullStack = "";
            string fullWeight = "";
            for (int z = 0; z < Ship.RowList.Count; z++)
            {
                if (z > 0)
                {
                    fullStack += '/';
                    fullWeight += '/';
                }

                for (int x = 0; x < Ship.RowList[z].StackList.Count; x++)
                {
                    if (x > 0)
                    {
                        fullStack += ",";
                        fullWeight += ",";
                    }

                    if (Ship.RowList[z].StackList[x].containers.Count > 0)
                    {
                        for (int y = 0; y < Ship.RowList[z].StackList[x].containers.Count; y++)
                        {
                            Container container = Ship.RowList[z].StackList[x].containers[y];

                            fullStack += Convert.ToString((int)container.ContainerType);
                            fullWeight += Convert.ToString(container.Weight);
                            if (y < (Ship.RowList[z].StackList[x].containers.Count - 1))
                            {
                                fullWeight += "-";
                            }
                        }
                    }
                }
            }

            Process.Start($"https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length=" + Ship.Length + "&width=" + Ship.Width + "&stacks=" + fullStack + "&weights=" + fullWeight + "");
            return $"https://i872272.luna.fhict.nl/ContainerVisualizer/index.html?length=" + Ship.Length + "&width=" + Ship.Width + "&stacks=" + fullStack + "&weights=" + fullWeight + "";
        }
    }
}
