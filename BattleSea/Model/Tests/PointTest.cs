using NUnit.Framework;
using BattleSea.Model;

namespace BattleSea.Model.Test
{
    [TestFixture]
    class PointTest
    {
        [Test]
        public void EqualsIsTrue()
        {
            var pointFirst = new Point(1, 1);
            var pointSecond = new Point(1, 1);
            Assert.IsTrue(pointFirst.Equals(pointSecond));
        }

        [Test]
        public void EqualsIsFalse()
        {
            var pointFirst = new Point(1, 1);
            var pointSecond = new Point(2, 2);
            Assert.IsFalse(pointFirst.Equals(pointSecond));
        }
    }
}
