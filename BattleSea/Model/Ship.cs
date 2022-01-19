using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleSea.Model
{
    class Ship
    {
        private Point point;

        public Ship(Point point)
        {
            this.point = point;
        }

        public Point GetPoint()
        {
            return point;
        }

        public int GetXPoint()
        {
            return point.X;
        }

        public int GetYPoint()
        {
            return point.Y;
        }

        public bool includePoint(Point point)
        {
            return this.point.Equals(point);
        }

        public bool CheckAppendShip(Point point)
        {
            for (int x = point.X - 1; x <= point.X + 1; x++)
            {
                for (int y = point.Y - 1; y <= point.Y + 1; y++)
                {
                    if (includePoint(new Point(x, y)))
                    {
                        throw new Exception("Корабль должен быть расположен на расстоянии в одну клетку");
                    }
                }
            }
            return true;
        }

        public bool Shoot(Point point)
        {           
                if (includePoint(point))
                {
                    this.point.Hit = true;
                    return true;
                }
            return false;
        }

        public bool IsKill()
        {
            return point.Hit;
        }

        public static Ship GenerateRandomShip()
        {            
            return new Ship(Point.GenerateRandom());
        }
    }
}
