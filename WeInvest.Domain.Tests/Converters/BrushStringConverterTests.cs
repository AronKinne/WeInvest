using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Domain.Converters;

namespace WeInvest.Domain.Tests.Converters {
    public class BrushStringConverterTests {

        private BrushStringConverter _converter;

        [SetUp]
        public void SetUp() {
            _converter = new BrushStringConverter();
        }

        [Test]
        public void BrushToString_ShouldReturnStringHexValue() {
            Brush brush = Brushes.Black;
            string expected = "#FF000000";

            var actual = _converter.BrushToString(brush);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void StringToBrush_ShouldReturnBrush() {
            string hex = "#FF000000";
            Brush expected = Brushes.Black;

            var actual = _converter.StringToBrush(hex);

            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

    }
}
