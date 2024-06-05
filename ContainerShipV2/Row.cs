using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerShipV2
{
    public class Row
    {
        List<Stack> stackList = new List<Stack>();

        public int Width { get; set; }
        public int MaxHeight { get; set; }
        public Side side { get; set; }

        public Row(int width, int RowSide, int maxHeight) 
        {
            Width = width;
            MaxHeight = maxHeight;
            side = (Side)RowSide;
        }

        public enum Side
        {
            Left = 1,
            Centre = 2,
            Right = 3,
        }
    }
}
