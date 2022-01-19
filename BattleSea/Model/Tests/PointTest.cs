using NUnit.Framework;
using BattleSea.Model;

namespace BattleSea.Model.Test
{
    [TestFixture]
    class PointTest
    {
        //
        // Проверка на совпадение точек 
        //
        [Test]
        public void EqualsIsTrue()
        {
            var pointFirst = new Point(1, 1);
            var pointSecond = new Point(1, 1);
            Assert.IsTrue(pointFirst.Equals(pointSecond));
        }
        //
        // Проверка на несовпадение точек 
        //
        [Test]
        public void EqualsIsFalse()
        {
            var pointFirst = new Point(1, 1);
            var pointSecond = new Point(2, 2);
            Assert.IsFalse(pointFirst.Equals(pointSecond));
        }
    }
}
