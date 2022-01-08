using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleSea.Model
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Hit { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Hit = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Point && ((Point)obj).Y == Y && ((Point)obj).X == X;
        }

        public static Point GenerateRandom()
        {
            Random random = new Random();
            return new Point(random.Next(1, 11), random.Next(10));
        }
    }
}
