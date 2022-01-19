using NUnit.Framework;
using BattleSea.Model;

namespace BattleSea.Model.Test
{
    [TestFixture]
    class BattleTest
    {
        public Battle battle = Battle.GenerateRandom();

        [SetUp]
        public void SetUp()
        {
            foreach (var ship in battle.GetShips())
            {
                ship.GetPoint().Hit = true;               
            }
        }

        //
        // Проверка на убийство всех кораблей ( все ли корабли убиты? ) 
        //
        [Test]
        public void IsAllShipKilledTest()
        {
            Assert.IsTrue(battle.IsAllShipDead());
        }
        //
        // Проверка на то, не все ли корабли убиты 
        //
        [Test]
        public void IsNotAllShipKIlledTest()
        {
            battle.GetShips()[0].GetPoint().Hit = false;
            Assert.IsFalse(battle.IsAllShipDead());
        }
    }
}
