using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShipV2
{
    public class Stack
    {
        List<Container> containers = new List<Container>();

        public ReadOnlyCollection<Container> ContainerListReadable
        {
            get { return containers.AsReadOnly(); }
        }
        public int MaxWeight { get; set; } = 150;
        public int ContainersWeight { get; set; }
        public int Position { get; set; }
        public bool IsFront { get; private set; }
        public bool Reserved { get; set; }
        public bool StackIsFull { get; set; }

        public Stack(int position, bool isfront)
        {
            Position = position;
            IsFront = isfront;
        }

        public bool TryAddingContainer(Container container)
        {
            if (Reserved)
            {
                return false;
            }

            if (container.Coolable && Position > 0)
            {
                return false;
            }

            if ((ContainersWeight + container.Weight) <= MaxWeight)
            {
                if (container.Valuable)
                {
                    if (containers.Count == 0)
                    {
                        containers.Add(container);
                    }
                    else
                    {
                        if (!containers[(containers.Count - 1)].Valuable)
                        {
                            containers.Add(container);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    containers.Insert(0, container);
                }

                ContainersWeight += container.Weight;


                if ((ContainersWeight + container.MinWeight) >= MaxWeight)
                {
                    StackIsFull = true;
                }

                return true;
            }

            return false;
        }


        public void SetReserved()
        {
            Reserved = true;
        }
    }
}
