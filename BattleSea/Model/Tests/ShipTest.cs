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

        [Test]
        public void IncludePointTestSuccess()
        {
            Assert.IsTrue(ship.includePoint(new Point(1, 1)));
        }

        [Test]
        public void CheckAppendShipIsFailure()
        {
            Assert.Throws<Exception>(() => ship.CheckAppendShip(new Point(2, 2)));
        }

        [Test]
        public void ShootShipSuccess()
        {
            Assert.IsTrue(ship.Shoot(new Point(1, 1)));
        }

        [Test]
        public void ShipIsKilled()
        {
            ship.Shoot(new Point(1, 1));
            Assert.IsTrue(ship.IsKill());
        }
    }
}
