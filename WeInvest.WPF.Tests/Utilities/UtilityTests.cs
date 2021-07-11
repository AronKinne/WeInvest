using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.WPF.Utilities;

namespace WeInvest.WPF.Tests.Utilities {
    public class UtilityTests {

        [TestCase(5, 0, 10, 0, 100, 50)]
        [TestCase(12, 10, 20, 30, 40, 32)]
        [TestCase(0.5, 0.5, 1.5, 9.9, 8.9, 9.9)]
        [TestCase(-12.5, -2.5, 7.5, 15, 10, 20)]
        public void Map_WithValidValues_ShouldWork(double value, double low1, double high1, double low2, double high2, double expected) {
            double actual = Utility.Map(value, low1, high1, low2, high2);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BrushesArray() {
            var array = Utility.BrushesArray;

            Assert.That(array.Length, Is.EqualTo(141));
            Assert.That(array[7], Is.EqualTo(Brushes.Black));
        }

        [TestCase(2, new double[] { 0, 1, 2, 3, 4, 5 }, 2)]
        [TestCase(1.2, new double[] { 0, 1, 2, 3, 4, 5 }, 1)]
        [TestCase(3.5, new double[] { 0, 1, 2, 3, 4, 5 }, 3)]
        [TestCase(8, new double[] { 0, 1, 2, 3, 4, 5 }, 5)]
        public void GetClosest_ShouldWork(double value, IEnumerable<double> numberPool, double expected) {
            double actual = Utility.GetClosest(value, numberPool);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
