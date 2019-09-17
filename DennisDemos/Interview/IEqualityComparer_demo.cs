﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Interview
{
    public class IEqualityComparer_demo
    {
        #region Run
        public static void Run()
        {
            BoxEqualityComparer boxEqC = new BoxEqualityComparer();

            var boxes = new Dictionary<Box, string>(boxEqC);

            var redBox = new Box(4, 3, 4);
            AddBox(boxes, redBox, "red");

            var blueBox = new Box(4, 3, 4);
            AddBox(boxes, blueBox, "blue");

            var greenBox = new Box(3, 4, 3);
            AddBox(boxes, greenBox, "green");
            Console.WriteLine();

            Console.WriteLine("The dictionary contains {0} Box objects.",
                              boxes.Count);
        }
        private static void AddBox(Dictionary<Box, String> dict, Box box, String name)
        {
            try
            {
                dict.Add(box, name);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Unable to add {0}: {1}", box, e.Message);
            }
        }
        #endregion
    }

    public class Box
    {
        public Box(int h, int l, int w)
        {
            this.Height = h;
            this.Length = l;
            this.Width = w;
        }

        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public override String ToString()
        {
            return String.Format("({0}, {1}, {2})", Height, Length, Width);
        }
    }

    class BoxEqualityComparer : IEqualityComparer<Box>
    {
        public bool Equals(Box b1, Box b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.Height == b2.Height && b1.Length == b2.Length
                                && b1.Width == b2.Width)
                return true;
            else
                return false;
        }

        public int GetHashCode(Box bx)
        {
            int hCode = bx.Height ^ bx.Length ^ bx.Width;
            return hCode.GetHashCode();
        }
    }
}
