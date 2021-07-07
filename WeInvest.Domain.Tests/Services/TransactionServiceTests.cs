using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Tests.Services {
    public class TransactionServiceTests {

        private TransactionService _transactionService;
        private Investor _emptyInvestor;

        [SetUp]
        public void SetUp() {
            var mockInvestorDataAccess = new Mock<IDataAccess<Investor>>();
            mockInvestorDataAccess.Setup(d => d.UpdateAsync(It.IsAny<int>(), It.IsAny<Investor>())).ReturnsAsync(It.IsAny<Investor>());
            _transactionService = new TransactionService(mockInvestorDataAccess.Object);

            var mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _emptyInvestor = new Investor(null, mockBrushStringConverter.Object) {
                Name = "Tester",
                Brush = Brushes.Black
            };
        }

        [Test]
        public async Task DepositAsync_FirstTime_ShouldReplaceFirstShareHistoryValue() {
            float amount = 10;

            await _transactionService.DepositAsync(_emptyInvestor, amount);

            Assert.That(_emptyInvestor.ShareHistory.Count, Is.EqualTo(1));
            Assert.That(_emptyInvestor.ShareHistory[0], Is.EqualTo(amount));
        }

        [Test]
        public async Task DepositAsync_SecondTime_ShouldAddUp() {
            float amount1 = 10;
            float amount2 = 20;

            await _transactionService.DepositAsync(_emptyInvestor, amount1);
            await _transactionService.DepositAsync(_emptyInvestor, amount2);

            Assert.That(_emptyInvestor.ShareHistory.Count, Is.EqualTo(2));
            Assert.That(_emptyInvestor.ShareHistory[1], Is.EqualTo(amount1 + amount2));
        }

    }
}
