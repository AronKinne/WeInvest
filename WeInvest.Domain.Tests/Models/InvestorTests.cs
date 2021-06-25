using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Models {
    public class InvestorTests {

        private Mock<IBrushStringConverter> _mockBrushStringConverter;
        private Investor _emptyInvestor;

        [SetUp]
        public void SetUp() {
            _mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _emptyInvestor = new Investor(null, _mockBrushStringConverter.Object) {
                Name = "Tester",
                Brush = Brushes.Black
            };
        }

        [Test]
        public void ShareHistoryString_Get() {
            var amounts = new float[] { 50, 20 };
            string expected = $"{amounts[0]} {amounts[0] + amounts[1]}";

            var mockListStringConverter = new Mock<IListStringConverter>();
            mockListStringConverter
                .Setup(c => c.ListToString(new List<float>() {
                    amounts[0],
                    amounts[0] + amounts[1]
                }))
                .Returns(expected);

            var investor = new Investor(mockListStringConverter.Object, _mockBrushStringConverter.Object);

            investor.Deposit(amounts[0]);
            investor.Deposit(amounts[1]);

            Assert.That(investor.ShareHistoryString, Is.EqualTo(expected));

            mockListStringConverter.VerifyAll();
        }

        [Test]
        public void ShareHistoryString_Set_ShouldSetShareHistory() {
            var amounts = new float[] { 50, 20 };
            string stringValue = $"{amounts[0]} {amounts[0] + amounts[1]}";

            var mockListStringConverter = new Mock<IListStringConverter>();
            mockListStringConverter
                .Setup(c => c.StringToList<float>(stringValue))
                .Returns(new List<float>() {
                    amounts[0],
                    amounts[0] + amounts[1]
                });

            var investor = new Investor(mockListStringConverter.Object, _mockBrushStringConverter.Object);

            investor.ShareHistoryString = stringValue;

            Assert.That(investor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(investor.ShareHistory[0], Is.EqualTo(amounts[0]));
            Assert.That(investor.ShareHistory[1], Is.EqualTo(amounts[0] + amounts[1]));

            mockListStringConverter.VerifyAll();
        }

        [Test]
        public void Brush_Get_ShouldGetBrushFromColorHex() {
            var hex = "#ff000000";
            var expected = Brushes.Black;

            _mockBrushStringConverter
                .Setup(c => c.StringToBrush(hex))
                .Returns(expected);

            var investor = new Investor(null, _mockBrushStringConverter.Object);
            investor.ColorHex = hex;

            Assert.That(investor.Brush, Is.EqualTo(expected));

            _mockBrushStringConverter.VerifyAll();
        }

        [Test]
        public void Brush_Set_ShouldSetColorHex() {
            var brush = Brushes.White;
            var expected = "#FFFFFFFF";

            _mockBrushStringConverter
                .Setup(c => c.BrushToString(brush))
                .Returns(expected);

            var investor = new Investor(null, _mockBrushStringConverter.Object);

            investor.Brush = brush;

            Assert.That(investor.ColorHex, Is.EqualTo(expected));

            _mockBrushStringConverter.VerifyAll();
        }

        [Test]
        public void Deposit_FirstTime_ShouldReplaceFirstShareHistoryValue() {
            float amount = 10;

            _emptyInvestor.Deposit(amount);

            Assert.That(_emptyInvestor.ShareHistory.Count, Is.EqualTo(1));
            Assert.That(_emptyInvestor.ShareHistory[0], Is.EqualTo(amount));
        }

        [Test]
        public void Deposit_SecondTime_ShouldAddUp() {
            float amount1 = 10;
            float amount2 = 20;

            _emptyInvestor.Deposit(amount1);
            _emptyInvestor.Deposit(amount2);

            Assert.That(_emptyInvestor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(_emptyInvestor.ShareHistory[1], Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void Share_NoDeposit_ShouldReturnZero() {
            Assert.That(_emptyInvestor.Share, Is.EqualTo(0));
        }

        [Test]
        public void Share_WithDeposit_ShouldReturnLastValueInShareHistory() {
            float amount1 = 10;
            float amount2 = 20;

            _emptyInvestor.Deposit(amount1);
            _emptyInvestor.Deposit(amount2);

            Assert.That(_emptyInvestor.Share, Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void ToString_WithMultipleShares() {
            string name = _emptyInvestor.Name;
            float amount1 = 10;
            float amount2 = 20;
            string expected = $"{name} ({amount1}, {amount1 + amount2})";

            _emptyInvestor.Deposit(amount1);
            _emptyInvestor.Deposit(amount2);
            string actual = _emptyInvestor.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
