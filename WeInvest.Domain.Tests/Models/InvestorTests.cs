using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.WPF.Utilities;

namespace WeInvest.Domain.Tests.Models {
    public class InvestorTests {

        private Investor _investor;

        [SetUp]
        public void SetUp() {
            var serviceProvider = ServiceProviderFactory.Create();
            var investorFactory = serviceProvider.GetRequiredService<IFactory<Investor>>();

            _investor = investorFactory.Create();
            _investor.Name = "Tester";
            _investor.Brush = Brushes.Black;
        }

        [Test]
        public void ShareHistoryString_Get() {
            _investor.Deposit(50);
            _investor.Deposit(20);
            string expected = "50 70";

            Assert.That(_investor.ShareHistoryString, Is.EqualTo(expected));
        }

        [Test]
        public void ShareHistoryString_Set_ShouldSetShareHistory() {
            _investor.ShareHistoryString = "50 70";

            Assert.That(_investor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(_investor.ShareHistory[0], Is.EqualTo(50));
            Assert.That(_investor.ShareHistory[1], Is.EqualTo(70));
        }

        [Test]
        public void Brush_Get_ShouldGetBrushFromColorHex() {
            Assert.That(_investor.Brush.ToString(), Is.EqualTo(Brushes.Black.ToString()));
        }

        [Test]
        public void Brush_Set_ShouldSetColorHex() {
            var expected = "#FFFFFFFF";
            _investor.Brush = Brushes.White;

            Assert.That(_investor.ColorHex, Is.EqualTo(expected));
        }

        [Test]
        public void Deposit_FirstTime_ShouldReplaceFirstShareHistoryValue() {
            float amount = 10;

            _investor.Deposit(amount);

            Assert.That(_investor.ShareHistory.Count, Is.EqualTo(1));
            Assert.That(_investor.ShareHistory[0], Is.EqualTo(amount));
        }

        [Test]
        public void Deposit_SecondTime_ShouldAddUp() {
            float amount1 = 10;
            float amount2 = 20;

            _investor.Deposit(amount1);
            _investor.Deposit(amount2);

            Assert.That(_investor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(_investor.ShareHistory[1], Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void Share_NoDeposit_ShouldReturnZero() {
            Assert.That(_investor.Share, Is.EqualTo(0));
        }

        [Test]
        public void Share_WithDeposit_ShouldReturnLastValueInShareHistory() {
            float amount1 = 10;
            float amount2 = 20;

            _investor.Deposit(amount1);
            _investor.Deposit(amount2);

            Assert.That(_investor.Share, Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void ToString_WithMultipleShares() {
            string name = _investor.Name;
            float amount1 = 10;
            float amount2 = 20;
            string expected = $"{name} ({amount1}, {amount1 + amount2})";

            _investor.Deposit(amount1);
            _investor.Deposit(amount2);
            string actual = _investor.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
