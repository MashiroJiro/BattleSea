using NUnit.Framework;
using BattleSea.Service;
using Moq;
using BattleSea.Model.Interfaces;
using BattleSea.Model;

namespace BattleSea.Test.Service
{
    [TestFixture]
    public class ShootServiceTest
    {
        private ShootService ShootService;
        Mock<IBattle> battle;
        Mock<IBattle> enemy;

        [SetUp]
        public void SetUp ()
        {
            battle = new Mock<IBattle>();
            enemy = new Mock<IBattle>();
            ShootService = new ShootService(battle.Object, enemy.Object);
        }

        //
        // Проверка на успешность выстрела 
        //
        [Test]
        public void ShootIsSuccess ()
        {
            battle.Setup(a => a.Shoot(new Point(1, 1), enemy.Object)).Returns(true);

            Assert.IsTrue(ShootService.Shoot(new Point(1, 1)));
        }
        //
        // Проверка на промах
        //
        [Test]
        public void ShootIsFalse()
        {
            battle.Setup(a => a.Shoot(new Point(1, 1), enemy.Object)).Returns(false);

            Assert.IsFalse(ShootService.Shoot(new Point(1, 1)));
        }
    }
}
