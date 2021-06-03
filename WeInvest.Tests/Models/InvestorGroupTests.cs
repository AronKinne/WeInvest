using NUnit.Framework;
using System;
using System.Windows.Media;
using WeInvest.Models;

namespace WeInvest.Tests.Models {
    class InvestorGroupTests {

        [Test]
        public void CurrentAccount_WithAddedAccount_ShouldReturnLastAccount() {
            InvestorGroup investorGroup = new InvestorGroup();
            Investor investor = investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            investorGroup.Deposit(investor, balance);

            Assert.That(investorGroup.CurrentAccount.ShareByInvestor.ContainsKey(investor));
            Assert.That(investorGroup.CurrentAccount.Balance, Is.EqualTo(balance));
        }

        [Test]
        public void AddInvestor_WithValidInput_ShouldReturnNewInvestor() {
            string name = "Tester";
            Brush color = Brushes.Black;
            InvestorGroup investorGroup = new InvestorGroup();

            var newInvestor = investorGroup.AddInvestor(name, color);

            Assert.That(newInvestor.Name, Is.EqualTo(name));
            Assert.That(newInvestor.Color, Is.EqualTo(color));
        }

        [Test]
        public void AddInvestor_WithExistingAccountHistory_ShouldAddInvestorToAccountHistory() {
            InvestorGroup investorGroup = new InvestorGroup();
            var investor1 = investorGroup.AddInvestor("Investor 1", Brushes.Black);

            investorGroup.Deposit(investor1, 10);

            Assert.That(investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 1);

            investorGroup.AddInvestor("Investor 2", Brushes.Black);

            Assert.That(investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 2);
        }

        [Test]
        public void Deposit_WithExistingInvestor_ShouldCreateNewAccount() {
            InvestorGroup investorGroup = new InvestorGroup();
            var investor = investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            investorGroup.Deposit(investor, balance);

            Assert.That(investor.Share, Is.EqualTo(balance));
            Assert.That(investorGroup.AccountHistory.Count, Is.EqualTo(1));
            Assert.That(investorGroup.AccountHistory[0].Balance, Is.EqualTo(balance));
        }

        [Test]
        public void Deposit_WithUnknownInvestor_ShouldThrowException() {
            InvestorGroup investorGroup = new InvestorGroup();
            investorGroup.AddInvestor("I exist", Brushes.Black);
            Investor stranger = new Investor("Stranger", Brushes.Black);

            Assert.Throws<ArgumentException>(() => investorGroup.Deposit(stranger, 10));
        }
    }
}
