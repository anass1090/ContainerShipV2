﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ContainerShipV2
{
    public class Row
    {
        public List<Stack> stackList = new List<Stack>();

        public int Width { get; set; }
        public int MaxHeight { get; set; }
        public Side side { get; set; }


        public Row(int width, int RowSide)
        {
            Width = width;
            stackList = GetAllStackInRow();
            side = (Side)RowSide;
        }

        public enum Side
        {
            Left = 1,
            Centre = 2,
            Right = 3,
        }

        public bool TryAddingContainer(Container container)
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                if (stackList[i].TryAddingContainer(container))
                {
                    if (CheckIfContainerNeedsReservedSpace(container, i))
                    {
                        return true;
                    }

                    return true;
                }
            }

            return false;
        }

        private bool CheckIfContainerNeedsReservedSpace(Container container, int index)
        {
            if ((int)container.containerType == 2)
            {
                if (stackList[index].IsBack || stackList[index].IsFront)
                {
                    return true;    
                }
                else if (!stackList[(index - 1)].Reserved && (index + 1) < (stackList.Count))
                {
                    stackList[index + 1].Reserved = true;
                    return true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        private List<Stack> GetAllStackInRow()
        {
            List<Stack> stacks = new List<Stack>();

            for (int i = 0; i < Width; i++) 
            {
                bool isFront = false;
                bool isBack = false;

                if (i == 0)
                {
                    isFront = true;
                }

                if ((i + 1) == Width)
                {
                    isBack = true;
                }

                stacks.Add(new Stack(i, isFront, isBack));
            }

            return stacks;
        }
    }

}

