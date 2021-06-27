using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.SQLite.DataAccess;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.Tests.DataAccess {
    public class InvestorDataAccessTests {

        private Mock<IFactory<IDbConnection>> _mockConnectionFactory;
        private Mock<IQueryService> _mockQueryService;
        private Mock<IFactory<Investor>> _mockInvestorFactory;

        private InvestorDataAccess _dataAccess;

        private Mock<IListStringConverter> _mockListStringConverter;
        private Mock<IBrushStringConverter> _mockBrushStringConverter;

        [SetUp]
        public void SetUp() {
            _mockConnectionFactory = new Mock<IFactory<IDbConnection>>();
            _mockQueryService = new Mock<IQueryService>();
            _mockInvestorFactory = new Mock<IFactory<Investor>>();

            _dataAccess = new InvestorDataAccess(_mockConnectionFactory.Object, _mockQueryService.Object, _mockInvestorFactory.Object);

            _mockListStringConverter = new Mock<IListStringConverter>();
            _mockBrushStringConverter = new Mock<IBrushStringConverter>();
        }

        [Test]
        public async Task CreateAsync_Always_ShouldReturnCreatedInvestor() {
            _mockConnectionFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(It.IsAny<IDbConnection>());

            _mockListStringConverter.Setup(c => c.ListToString(It.IsAny<IList<float>>())).Returns("0");
            _mockListStringConverter.Setup(c => c.StringToList<float>("0")).Returns(new List<float>() { 0 });

            _mockBrushStringConverter.Setup(c => c.BrushToString(Brushes.Black)).Returns("#ff000000");
            _mockBrushStringConverter.Setup(c => c.StringToBrush("#ff000000")).Returns(Brushes.Black);

            var insertedInvestor = new Investor(_mockListStringConverter.Object, _mockBrushStringConverter.Object) {
                Name = "Tester",
                Brush = Brushes.Black
            };

            string expectedSql = "INSERT INTO Investor (Name, ColorHex, ShareHistoryString) VALUES (@Name, @ColorHex, @ShareHistoryString);" +
                " SELECT * FROM Investor WHERE Id = last_insert_rowid();";

            var expectedInvestor = new Investor(_mockListStringConverter.Object, _mockBrushStringConverter.Object) {
                Id = 1,
                Name = "Tester",
                ColorHex = "#ff000000",
                ShareHistoryString = "0"
            };

            _mockQueryService.Setup(s => s.QuerySingleAsync(It.IsAny<IDbConnection>(), expectedSql, insertedInvestor)).Returns(Task.FromResult((object)expectedInvestor));
            _mockInvestorFactory.Setup(f => f.Create(It.IsAny<object>())).Returns(expectedInvestor);

            var createdInvestor = await _dataAccess.CreateAsync(insertedInvestor);

            Assert.That(createdInvestor, Is.EqualTo(expectedInvestor));
        }
    
    }
}
