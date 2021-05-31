using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Models;

namespace WeInvest.Tests.Models {
    class InvestorTests {

        [Test]
        public void Deposit_FirstTime_ShouldReplaceFirstShareHistoryValue() {
            Investor investor = new Investor("Tester", Brushes.Black);
            float amount = 10;

            investor.Deposit(amount);

            Assert.That(investor.ShareHistory.Count, Is.EqualTo(1));
            Assert.That(investor.ShareHistory[0], Is.EqualTo(amount));
        }

        [Test]
        public void Deposit_SecondTime_ShouldAddUp() {
            Investor investor = new Investor("Tester", Brushes.Black);
            float amount1 = 10;
            float amount2 = 20;

            investor.Deposit(amount1);
            investor.Deposit(amount2);

            Assert.That(investor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(investor.ShareHistory[1], Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void Share_NoDeposit_ShouldReturnZero() {
            Investor investor = new Investor("Tester", Brushes.Black);

            Assert.That(investor.Share, Is.EqualTo(0));
        }

        [Test]
        public void Share_WithDeposit_ShouldReturnLastValueInShareHistory() {
            Investor investor = new Investor("Tester", Brushes.Black);
            float amount1 = 10;
            float amount2 = 20;

            investor.Deposit(amount1);
            investor.Deposit(amount2);

            Assert.That(investor.Share, Is.EqualTo(amount1 + amount2));
        }
    }
}
