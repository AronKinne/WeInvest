using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WeInvest.Models;
using WeInvest.Utilities.Factories;

namespace WeInvest.Tests.Utilities.Factories {
    public class InvestorGroupFactoryTests {

        private InvestorGroupFactory _investorGroupFactory;

        [SetUp]
        public void SetUp() {
            var serviceProvider = ServiceProviderFactory.Create();
            _investorGroupFactory = serviceProvider.GetRequiredService<IFactory<InvestorGroup>>() as InvestorGroupFactory;
        }

        [Test]
        public void Create_ShouldReturnNewInvestor() {
            var result = _investorGroupFactory.Create();

            Assert.That(result is InvestorGroup);
        }
    }
}
