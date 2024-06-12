using System.Collections.Generic;

namespace ClassLibraryContainerShip
{
    public class Row
    {
        private List<Stack> stacks;
        public List<Stack> StackList { get { return InitializeStacks(); } }
        public int Width { get; set; }
        public Sides Side { get; set; }

        public Row(int width, Sides side)
        {
            Width = width;
            Side = side;
        }

        public enum Sides
        {
            Left = 1,
            Centre = 2,
            Right = 3,
        }

        public bool TryAddingContainer(Container container)
        {
            for (int i = 0; i < StackList.Count; i++)
            {
                if (StackList[i].TryAddingContainer(container))
                {
                    ReserveStackForValuableContainers(container, i);
                    return true;                   
                }
            }

            return false;
        }

        private void ReserveStackForValuableContainers(Container container, int index)
        {
            if (container.ContainerType == Container.ContainerTypes.Valuable || container.ContainerType == Container.ContainerTypes.CoolableValuable)
            {
                if (index > 0 && !StackList[index - 1].Reserved && index + 1 < StackList.Count)
                {
                    StackList[index + 1].Reserved = true;
                }
            }          
        }

        private List<Stack> InitializeStacks()
        {
            if (stacks == null)
            {
                stacks = new List<Stack>();
                for (int i = 0; i < Width; i++)
                {
                    if (i == 0)
                    {
                        stacks.Add(new Stack(i, true, false));
                    }
                    else if ((i + 1) == Width)
                    {
                        stacks.Add(new Stack(i, false, true));
                    }
                    else
                    {
                        stacks.Add(new Stack(i, false, false));
                    }
                }
            }

            return stacks;
        }
    }
}

