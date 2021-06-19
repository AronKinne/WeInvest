using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Utilities.Services;

namespace WeInvest.Tests.Utilities.Services {
    public class BrushConvertingServiceTests {

        private BrushConvertingService _service;

        [SetUp]
        public void SetUp() {
            _service = new BrushConvertingService();
        }

        [Test]
        public void BrushToString_ShouldReturnStringHexValue() {
            Brush brush = Brushes.Black;
            string expected = "#FF000000";

            var actual = _service.BrushToString(brush);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void StringToBrush_ShouldReturnBrush() {
            string hex = "#FF000000";
            Brush expected = Brushes.Black;

            var actual = _service.StringToBrush(hex);

            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

    }
}
