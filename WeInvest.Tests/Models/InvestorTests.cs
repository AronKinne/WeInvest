using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Models;

namespace WeInvest.Tests.Models {
    [TestFixture]
    public class InvestorTests {

        [Test]
        public void Deposit_FirstTime() {
            Investor investor = new Investor("Tester", Brushes.Black);
            float amount = 10;

            investor.Deposit(amount);

            Assert.That(investor.ShareHistory.Count, Is.EqualTo(1));
            Assert.That(investor.ShareHistory[0], Is.EqualTo(amount));
        }

    }
}
