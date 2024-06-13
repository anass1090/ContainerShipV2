using System.Collections.Generic;
using System.Linq;

namespace ClassLibraryContainerShip
{
    public class Stack
    {
        public List<Container> containers = new List<Container>();

        public int Position { get; set; }
        public bool IsFront { get; private set; }
        public bool IsBack { get; private set; }
        public bool Reserved { get; set; }

        private readonly int BaseMaxWeight = 120;
        public int MaxWeight
        {
            get
            {
                if (containers.Count > 0)
                {
                    return BaseMaxWeight + containers[0].Weight;
                }
                return BaseMaxWeight;
            }
        }

        public int ContainersWeight
        {
            get
            {
                int totalWeight = 0;
                foreach (Container container in containers)
                {
                    totalWeight += container.Weight;
                }
                return totalWeight;
            }
        }

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
                container.UnfitReason = Container.UnfitReasons.Reserved;
                return false;
            }
            else if (container.ContainerType == Container.ContainerTypes.Coolable && Position > 0)
            {
                container.UnfitReason = Container.UnfitReasons.TooManyCoolables;
                return false;
            } else if(container.ContainerType == Container.ContainerTypes.CoolableValuable && Position > 0)
            {
                container.UnfitReason = Container.UnfitReasons.TooManyCoolableValuables;
                return false;
            }

            if (ContainersWeight + container.Weight <= MaxWeight)
            {
                if (container.ContainerType == Container.ContainerTypes.Valuable || container.ContainerType == Container.ContainerTypes.CoolableValuable)
                {
                    if (containers.Count == 0 || 
                        containers.LastOrDefault().ContainerType != Container.ContainerTypes.Valuable &&
                        containers.LastOrDefault().ContainerType != Container.ContainerTypes.CoolableValuable)
                    {
                        containers.Add(container);
                    }
                    else
                    {
                        container.UnfitReason = Container.UnfitReasons.TooManyValuables;
                        return false;
                    }
                }
                else
                {
                    containers.Insert(0, container);
                }

                return true;
            }
            else
            {
                container.UnfitReason = Container.UnfitReasons.ExceedsMaxWeight;
                return false;
            }
        }
    }
}
