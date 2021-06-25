using NUnit.Framework;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Factories {
    public class InvestorGroupFactoryTests {

        private InvestorGroupFactory _investorGroupFactory;

        [SetUp]
        public void SetUp() {
            _investorGroupFactory = new InvestorGroupFactory(null, null);
        }

        [Test]
        public void Create_ShouldReturnNewInvestor() {
            var result = _investorGroupFactory.Create();

            Assert.That(result is InvestorGroup);
        }
    }
}
