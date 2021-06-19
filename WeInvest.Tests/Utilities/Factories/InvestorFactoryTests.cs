using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Models;
using WeInvest.Utilities.Factories;

namespace WeInvest.Tests.Utilities.Factories {
    public class InvestorFactoryTests {

        private InvestorFactory _investorFactory;

        [SetUp]
        public void SetUp() {
            var serviceProvider = ServiceProviderFactory.Create();
            _investorFactory = serviceProvider.GetRequiredService<IFactory<Investor>>() as InvestorFactory;
        }

        [Test]
        public void Create_ShouldReturnNewInvestor() {
            var result = _investorFactory.Create();

            Assert.That(result is Investor);
        }

        [Test]
        public void Create_WithNameAndBrush_ShouldReturnNewInvestorWithProperties() {
            string name = "Tester";
            var brush = Brushes.Black;

            var result = _investorFactory.Create(name, brush);

            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Brush.ToString(), Is.EqualTo(brush.ToString()));
        }
    
    }
}
