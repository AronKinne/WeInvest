﻿using NUnit.Framework;
using WeInvest.Utilities.Services;

namespace WeInvest.Tests.Utilities.Services {
    public class ListConvertingServiceTests {

        private ListConvertingService _service;

        [SetUp]
        public void SetUp() {
            _service = new ListConvertingService();
        }

        [Test]
        public void ListToString_WithEmptyList_ShouldReturnEmptyString() {
            var list = new int[0];
            string expected = "";

            var actual = _service.ListToString<int>(list);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ListToString_WithFilledList_ShouldReturnString() {
            var list = new float[] { 2, 10, 5, 9.5f };
            string expected = "2 10 5 9,5";

            var actual = _service.ListToString<float>(list);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void StringToList_ShouldReturnIList() {
            var values = "2 10 5 9,5";
            var expected = new float[] { 2, 10, 5, 9.5f };

            var actual = _service.StringToList<float>(values);

            Assert.That(actual, Is.EqualTo(expected));
        }
    
    }
}
