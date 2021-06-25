using Moq;
using NUnit.Framework;
using System;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Models {
    public class InvestorGroupTests {

        private Mock<IBrushStringConverter> _mockBrushStringConverter;
        private Mock<IFactory<Investor>> _mockInvestorFactory;
        private Mock<IFactory<Account>> _mockAccountFactory;
        private InvestorGroup _investorGroup;

        [SetUp]
        public void SetUp() {
            _mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _mockBrushStringConverter
                .Setup(c => c.BrushToString(Brushes.Black))
                .Returns("#ff000000");
            _mockBrushStringConverter
                .Setup(c => c.StringToBrush("#ff000000"))
                .Returns(Brushes.Black);

            _mockInvestorFactory = new Mock<IFactory<Investor>>();
            _mockInvestorFactory
                .Setup(f => f.Create())
                .Returns(() => new Investor(null, _mockBrushStringConverter.Object));

            _mockAccountFactory = new Mock<IFactory<Account>>();
            _mockAccountFactory
                .Setup(f => f.Create())
                .Returns(() => new Account(null, null));

            _investorGroup = new InvestorGroup(_mockInvestorFactory.Object, _mockAccountFactory.Object);
        }

        [Test]
        public void CurrentAccount_WithAddedAccount_ShouldReturnLastAccount() {
            Investor investor = _investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            _investorGroup.Deposit(investor, balance);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.ContainsKey(investor));
            Assert.That(_investorGroup.CurrentAccount.Balance, Is.EqualTo(balance));

            _mockInvestorFactory.VerifyAll();
            _mockAccountFactory.VerifyAll();
        }

        [Test]
        public void AddInvestor_WithNameAndBrush_ShouldReturnNewInvestor() {
            string name = "Tester";
            Brush brush = Brushes.Black;

            var newInvestor = _investorGroup.AddInvestor(name, brush);

            Assert.That(newInvestor.Name, Is.EqualTo(name));
            Assert.That(newInvestor.Brush, Is.EqualTo(brush));

            _mockInvestorFactory.VerifyAll();
            _mockBrushStringConverter.VerifyAll();
        }

        [Test]
        public void AddInvestor_WithInvestor_ShouldAddInvestor() {
            var investor = new Investor(null, _mockBrushStringConverter.Object);

            _investorGroup.AddInvestor(investor);

            Assert.That(_investorGroup.Investors.Contains(investor));
        }

        [Test]
        public void AddInvestor_WithExistingAccountHistory_ShouldAddInvestorToAccountHistory() {
            var investor1 = _investorGroup.AddInvestor("Investor 1", Brushes.Black);

            _investorGroup.Deposit(investor1, 10);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 1);

            _investorGroup.AddInvestor("Investor 2", Brushes.Black);

            Assert.That(_investorGroup.CurrentAccount.ShareByInvestor.Keys.Count == 2);

            _mockInvestorFactory.VerifyAll();
            _mockAccountFactory.VerifyAll();
            _mockBrushStringConverter.Verify(c => c.BrushToString(Brushes.Black), Times.Exactly(2));
        }

        [Test]
        public void Deposit_WithExistingInvestor_ShouldCreateNewAccount() {
            var investor = _investorGroup.AddInvestor("Tester", Brushes.Black);
            float balance = 10;

            _investorGroup.Deposit(investor, balance);

            Assert.That(investor.Share, Is.EqualTo(balance));
            Assert.That(_investorGroup.AccountHistory.Count, Is.EqualTo(1));
            Assert.That(_investorGroup.AccountHistory[0].Balance, Is.EqualTo(balance));

            _mockInvestorFactory.VerifyAll();
            _mockAccountFactory.VerifyAll();
            _mockBrushStringConverter.Verify(c => c.BrushToString(Brushes.Black), Times.Once);
        }

        [Test]
        public void Deposit_WithUnknownInvestor_ShouldThrowException() {
            _investorGroup.AddInvestor("I exist", Brushes.Black);
            Investor stranger = new Investor(null, _mockBrushStringConverter.Object) {
                Name = "Stranger",
                Brush = Brushes.Black
            };

            Assert.Throws<ArgumentException>(() => _investorGroup.Deposit(stranger, 10));

            _mockInvestorFactory.VerifyAll();
            _mockBrushStringConverter.Verify(c => c.BrushToString(Brushes.Black), Times.Exactly(2));
        }
    }
}
