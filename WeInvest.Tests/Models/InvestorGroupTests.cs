using NUnit.Framework;
using System;
using System.Windows.Media;
using WeInvest.Models;

namespace WeInvest.Tests.Models {
    public class InvestorGroupTests {

        private InvestorGroup _investorGroup;

        [SetUp]
        public void SetUp() {
            _investorGroup = new InvestorGroup();
        }

        [Test]
        public void CurrentAccount_WithAddedAccount_ShouldReturnLastAccount() {
            Investor investor = _investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            _investorGroup.Deposit(investor, balance);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.ContainsKey(investor));
            Assert.That(_investorGroup.CurrentAccount.Balance, Is.EqualTo(balance));
        }

        [Test]
        public void AddInvestor_WithValidInput_ShouldReturnNewInvestor() {
            string name = "Tester";
            Brush color = Brushes.Black;

            var newInvestor = _investorGroup.AddInvestor(name, color);

            Assert.That(newInvestor.Name, Is.EqualTo(name));
            Assert.That(newInvestor.Brush, Is.EqualTo(color));
        }

        [Test]
        public void AddInvestor_WithExistingAccountHistory_ShouldAddInvestorToAccountHistory() {
            var investor1 = _investorGroup.AddInvestor("Investor 1", Brushes.Black);

            _investorGroup.Deposit(investor1, 10);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 1);

            _investorGroup.AddInvestor("Investor 2", Brushes.Black);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 2);
        }

        [Test]
        public void Deposit_WithExistingInvestor_ShouldCreateNewAccount() {
            var investor = _investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            _investorGroup.Deposit(investor, balance);

            Assert.That(investor.Share, Is.EqualTo(balance));
            Assert.That(_investorGroup.AccountHistory.Count, Is.EqualTo(1));
            Assert.That(_investorGroup.AccountHistory[0].Balance, Is.EqualTo(balance));
        }

        [Test]
        public void Deposit_WithUnknownInvestor_ShouldThrowException() {
            _investorGroup.AddInvestor("I exist", Brushes.Black);
            Investor stranger = new Investor() { Name = "Stranger", Brush = Brushes.Black };

            Assert.Throws<ArgumentException>(() => _investorGroup.Deposit(stranger, 10));
        }
    }
}
