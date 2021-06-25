using NUnit.Framework;
using System.Collections.Generic;
using WeInvest.Domain.Converters;

namespace WeInvest.Domain.Tests.Converters {
    public class DictionaryStringConverterTests {

        private DictionaryStringConverter _converter;

        [SetUp]
        public void SetUp() {
            _converter = new DictionaryStringConverter(new ListStringConverter());
        }

        [Test]
        public void DictionaryToString_ShouldReturnString() {
            IDictionary<int, double> dictionary = new Dictionary<int, double>();
            dictionary.Add(0, .8);
            dictionary.Add(1, .2);
            string expected = "0|0,8 1|0,2";

            var actual = _converter.DictionaryToString(dictionary);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void StringToDictionary_ShouldReturnDictionary() {
            string value = "0|0,8 1|0,2";

            var result = _converter.StringToDictionary<int, double>(value);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(0.8));
            Assert.That(result[1], Is.EqualTo(0.2));
        }

    }
}
