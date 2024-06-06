using System.Collections.Generic;

namespace ContainerShipV2
{
    public class Stack
    {
        public List<Container> containers = new List<Container>();

        public int MaxWeight { get; set; } = 150;
        public int ContainersWeight { get; set; }
        public int Position { get; set; }
        public bool IsFront { get; private set; }
        public bool IsBack { get; private set; }
        public bool Reserved { get; set; }
        public bool StackIsFull { get; set; }

        public Stack(int position, bool isfront, bool isBack)
        {
            Position = position;
            IsFront = isfront;
            IsBack = isBack;
        }

        public bool TryAddingContainer(Container container)
        {
            if (Reserved)
            {
                return false;
            }

            if (container.ContainerType == Container.ContainerTypes.Coolable && Position > 0)
            {
                return false;
            }

            if ((ContainersWeight + container.Weight) <= MaxWeight)
            {
                if (container.ContainerType == Container.ContainerTypes.Valuable)
                {
                    if (containers.Count == 0)
                    {
                        containers.Add(container);
                    }
                    else
                    {
                        if ((containers[(containers.Count - 1)].ContainerType != Container.ContainerTypes.Valuable))
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
    }
}
