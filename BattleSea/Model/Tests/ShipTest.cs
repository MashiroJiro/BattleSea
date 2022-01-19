using NUnit.Framework;
using BattleSea.Model;
using System;

namespace BattleSea.Model.Test
{
    [TestFixture]
    class ShipTest
    {
        private Ship ship;

        [SetUp]
        public void SetUp ()
        {
            ship = new Ship(new Point(1, 1));
        }
        //
        // Проверка на совпадение точки корабля с входной точкой
        //        
        [Test]
        public void IncludePointTestSuccess()
        {
            Assert.IsTrue(ship.includePoint(new Point(1, 1)));
        }
        //
        // Неуспешная проверка добавления корабля
        //
        [Test]
        public void CheckAppendShipIsFailure()
        {
            Assert.Throws<Exception>(() => ship.CheckAppendShip(new Point(2, 2)));
        }
        //
        // Проверка попадания в корабль 
        //
        [Test]
        public void ShootShipSuccess()
        {
            Assert.IsTrue(ship.Shoot(new Point(1, 1)));
        }
        //
        // Проверка, убит ли корабль 
        //
        [Test]
        public void ShipIsKilled()
        {
            ship.Shoot(new Point(1, 1));
            Assert.IsTrue(ship.IsKill());
        }
    }
}
