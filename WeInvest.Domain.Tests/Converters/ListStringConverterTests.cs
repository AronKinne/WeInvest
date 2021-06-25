using NUnit.Framework;
using WeInvest.Domain.Converters;

namespace WeInvest.Domain.Tests.Converters {
    public class ListStringConverterTests {

        private ListStringConverter _converter;

        [SetUp]
        public void SetUp() {
            _converter = new ListStringConverter();
        }

        [Test]
        public void ListToString_WithEmptyList_ShouldReturnEmptyString() {
            var list = new int[0];
            string expected = "";

            var actual = _converter.ListToString(list);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ListToString_WithFilledList_ShouldReturnString() {
            var list = new float[] { 2, 10, 5, 9.5f };
            string expected = "2 10 5 9,5";

            var actual = _converter.ListToString(list);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void StringToList_ShouldReturnIList() {
            var values = "2 10 5 9,5";
            var expected = new float[] { 2, 10, 5, 9.5f };

            var actual = _converter.StringToList<float>(values);

            Assert.That(actual, Is.EqualTo(expected));
        }
    
    }
}
