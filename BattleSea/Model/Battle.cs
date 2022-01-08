using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleSea.Model.Interfaces;

namespace BattleSea.Model
{
    class Battle : IBattle
    {
        private List<Ship> ships = new List<Ship>();
        private int count = 0;
        List<Point> shootPoints = new List<Point>();

        public bool Shoot(Point point, IBattle enemy)
        {
            if (shootPoints.IndexOf(point) != -1)
            {
                throw new Exception("Вы уже стреляли в это поле!");
            }
            bool isHit = false;
            List<Ship> ships = enemy.GetShips();
            foreach(var ship in ships)
            {
                if (ship.Shoot(point))
                {
                    point.Hit = true;
                    isHit = true;
                    break;
                }
            }
            shootPoints.Add(point);
            return isHit;
        }

        public List<Ship> GetShips()
        {
            return ships;
        }

        public Ship CreateShip(Point start)
        {
            Ship ship = new Ship(start);
            this.AddShip(ship);
            return ship;
        }

        private int AddShip(Ship ship)
        {
            if (count == 10)
            {
                throw new Exception("Вы уже жобавили все корабли этого размера");
            }
            CheckAppendShip(ship);
            ships.Add(ship);
            return ++count;
        }

        public bool IsAllShipAdded()
        {
            return count == 10;
        }

        public bool IsAllShipDead()
        {
            foreach (var ship in ships)
            {
                if (!ship.IsKill())
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckAppendShip(Ship ship)
        {
            foreach (var i in ships)
            {
                i.CheckAppendShip(ship.GetPoint());
            }
        }

        public static Battle GenerateRandom()
        {
            Battle battle = new Battle();
            
            while (!battle.IsAllShipAdded())
            {
                try
                {
                    battle.AddShip(Ship.GenerateRandomShip());
                } catch (Exception) { }
            }
            return battle;
        }
    }
}
