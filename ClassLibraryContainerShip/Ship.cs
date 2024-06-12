using System.Collections.Generic;

namespace ClassLibraryContainerShip
{
    public class Ship
    {
        public List<Container> Containers = new List<Container>();
        public List<Container> UnfitContainers = new List<Container>();
        private List<Row> Rows;
        public List<Row> RowList { get { return InitializeRows(); } }

        public int Width { get; private set; }
        public int Length { get; private set; }
        public int MaxWeight { get; private set; }
        public int MinWeight { get; private set; }
        public int TotalSlots { get; private set; }

        public int WeightLeft { get; set; }
        public int WeightRight { get; set; }

        public double WeightDifferencePercentage
        {
            get
            {
                return ((WeightLeft / TotalWeight) * 100) - ((WeightRight / TotalWeight) * 100);
            }
        }

        public int TotalWeight
        {
            get
            {
                int totalWeight = 0;
                foreach (Container container in Containers)
                {
                    totalWeight += container.Weight;
                }
                return totalWeight;
            }
        }

        public Ship(int lenght, int width)
        {
            Width = width;
            Length = lenght;
            MaxWeight = (lenght * width) * 150;
            MinWeight = MaxWeight / 2;
        }

        private Row.Sides CalculateEvenRows(int i)
        {
            Row.Sides side;
            if (i < Width / 2)
            {
                side = Row.Sides.Left;
            }
            else
            {
                side = Row.Sides.Right;
            }

            return side;
        }

        private Row.Sides CalculateUnevenRows(int i)
        {
            Row.Sides side;

            if (i < Width / 2)
            {
                side = Row.Sides.Left;
            }
            else if (i > Width / 2)
            {
                side = Row.Sides.Right;
            }
            else
            {
                side = Row.Sides.Centre;
            }

            return side;
        }

        public List<Row> InitializeRows()
        {
            if (Rows == null)
            {
                Rows = new List<Row>();

                for (int i = 0; i < Width; i++)
                {
                    Row.Sides side;

                    if (Width % 2 == 0)
                    {
                        side = CalculateEvenRows(i);
                    }
                    else
                    {
                        side = CalculateUnevenRows(i);
                    }

                    Rows.Add(new Row(Length, side));
                }
            }

            return Rows;
        }
    }
}
