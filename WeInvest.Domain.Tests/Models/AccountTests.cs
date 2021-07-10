using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Tests.Models {
    public class AccountTests {

        private Mock<IDictionaryStringConverter> _mockDictionaryStringConverter;
        private Mock<IDataAccess<Investor>> _mockInvestorDataAccess;
        private Account _account;
        private Investor _investorTester, _investor1, _investor2;
        private IList<Investor> _investorList;

        [SetUp]
        public void SetUp() {
            _mockDictionaryStringConverter = new Mock<IDictionaryStringConverter>();
            _mockInvestorDataAccess = new Mock<IDataAccess<Investor>>();
            _account = new Account(_mockDictionaryStringConverter.Object, _mockInvestorDataAccess.Object);

            var mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _investorTester = new Investor(null, mockBrushStringConverter.Object) {
                Name = "Tester",
                Brush = Brushes.Black
            };
            _investor1 = new Investor(null, mockBrushStringConverter.Object) {
                Id = 0,
                Name = "Investor 1",
                Brush = Brushes.Black
            };
            _investor2 = new Investor(null, mockBrushStringConverter.Object) {
                Id = 1,
                Name = "Investor 2",
                Brush = Brushes.White
            };

            _investorList = new List<Investor>() { _investor1, _investor2 };
        }

        [Test]
        public void ShareByInvestorString_Get_ShouldReturnConvertedShareByInvestorDictionary() {
            float amount1 = 10;
            float amount2 = 20;

            DespositInvestorList(amount1, amount2);

            _mockDictionaryStringConverter
                .Setup(c => c.DictionaryToString<int, float>(new Dictionary<int, float>() {
                    { 0, amount1 },
                    { 1, amount2 }
                }))
                .Returns($"0|{amount1} 1|{amount2}");

            _account.AddOwners(_investorList);

            var expected = $"0|{amount1} 1|{amount2}";

            Assert.That(_account.ShareByInvestorString, Is.EqualTo(expected));
            _mockDictionaryStringConverter.VerifyAll();
        }

        [Test]
        public void ShareByInvestorString_Set_ShouldSetShareByInvestorDictionary() {
            var amounts = new float[] { 10, 20 };

            DespositInvestorList(amounts[0], amounts[1]);

            string stringValue = $"0|{amounts[0]} 1|{amounts[1]}";
            
            _mockDictionaryStringConverter
                .Setup(c => c.StringToDictionary<int, float>(stringValue))
                .Returns(new Dictionary<int, float>() {
                    { 0, amounts[0] },
                    { 1, amounts[1] }
                });

            _mockInvestorDataAccess
                .Setup(s => s.GetAsync(It.IsAny<int>()))
                .Returns<int>(i => Task.FromResult(_investorList[i]));

            _account.ShareByInvestorString = stringValue;

            for(int i = 0; i < _account.ShareByInvestor.Count; i++) {
                Assert.That(_account.ShareByInvestor.ContainsKey(_investorList[i]));
                Assert.That(_account.ShareByInvestor[_investorList[i]], Is.EqualTo(amounts[i]));
            }

            _mockDictionaryStringConverter.VerifyAll();
            _mockInvestorDataAccess.VerifyAll();
        }

        [Test]
        public void Balance_WithShareByInvestorBeeingNull_ShouldReturnZero() {
            _account.ShareByInvestor = null;

            Assert.That(_account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Balance_WithValidInvestors_ShouldReturnSumOfInvestorsBalance() {
            float amount1 = 10;
            float amount2 = 20;

            DespositInvestorList(amount1, amount2);

            _account.AddOwners(_investorList);

            Assert.That(_account.Balance, Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void AddOwners_ShouldAdd() {
            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(0));

            _account.AddOwners(_investorList);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(_investorList.Count));
        }

        [Test]
        public void AddOwner_WithExistingInvestor_ShouldNotAdd() {
            _account.AddOwners(new List<Investor>() { _investorTester });

            _account.AddOwner(_investorTester, 10);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_account.ShareByInvestor.ContainsKey(_investorTester));
        }

        [Test]
        public void AddOwner_WithNewInvestor_ShouldAdd() {
            float balance = 10;

            _account.AddOwner(_investorTester, balance);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_account.ShareByInvestor[_investorTester], Is.EqualTo(balance));
        }

        [Test]
        public void RemoveOwner_WithExistingInvester_ShouldRemove() {
            float amount1 = 10;
            float amount2 = 20;

            DespositInvestorList(amount1, amount2);
            _account.AddOwners(_investorList);

            _mockDictionaryStringConverter
                .Setup(c => c.DictionaryToString(new Dictionary<int, float>() {
                    { _investorList[0].Id, amount1 }
                }))
                .Returns($"{_investorList[0].Id}|{amount1}");

            _account.RemoveOwner(_investorList[1].Id);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_account.ShareByInvestor[_investorList[0]], Is.EqualTo(amount1));
            Assert.That(_account.ShareByInvestorString, Is.EqualTo($"{_investorList[0].Id}|{amount1}"));
        }

        private void DespositInvestorList(float amount1, float amount2) {
            _investorList[0].ShareHistory = new List<float>() { amount1 };
            _investorList[1].ShareHistory = new List<float>() { amount2 };
        }

    }
}
