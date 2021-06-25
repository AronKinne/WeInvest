using Moq;
using NUnit.Framework;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Factories {
    public class InvestorFactoryTests {

        private Mock<IBrushStringConverter> _mockBrushStringConverter;
        private InvestorFactory _investorFactory;

        [SetUp]
        public void SetUp() {
            _mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _mockBrushStringConverter
                .Setup(c => c.BrushToString(Brushes.Black))
                .Returns("#ff000000");
            _mockBrushStringConverter
                .Setup(c => c.StringToBrush("#ff000000"))
                .Returns(Brushes.Black);

            _investorFactory = new InvestorFactory(null, _mockBrushStringConverter.Object);
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

            var result = _investorFactory.Create(new {
                Name = name,
                Brush = brush
            });

            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Brush.ToString(), Is.EqualTo(brush.ToString()));

            _mockBrushStringConverter.VerifyAll();
        }
    
    }
}
