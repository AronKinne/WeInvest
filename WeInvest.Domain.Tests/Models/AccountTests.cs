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

        private Account _emptyAccount;
        private Investor _investorTester, _investor1, _investor2;
        private IList<Investor> _investorList;

        [SetUp]
        public void SetUp() {
            _emptyAccount = new Account(null, null);

            var mockBrushStringConverter = new Mock<IBrushStringConverter>();
            _investorTester = new Investor(null, mockBrushStringConverter.Object) {
                Name = "Tester",
                Brush = Brushes.Black
            };
            _investor1 = new Investor(null, mockBrushStringConverter.Object) {
                Id = 0,
                Name = "1",
                Brush = Brushes.Black
            };
            _investor2 = new Investor(null, mockBrushStringConverter.Object) {
                Id = 1,
                Name = "2",
                Brush = Brushes.White
            };

            _investorList = new List<Investor>() { _investor1, _investor2 };
        }

        [Test]
        public void ShareByInvestorString_Get_ShouldReturnConvertedShareByInvestorDictionary() {
            float amount1 = 10;
            float amount2 = 20;

            _investorList[0].Deposit(amount1);
            _investorList[1].Deposit(amount2);

            var mockDictionaryStringConverter = new Mock<IDictionaryStringConverter>();
            mockDictionaryStringConverter
                .Setup(c => c.DictionaryToString<int, float>(new Dictionary<int, float>() {
                    { 0, amount1 },
                    { 1, amount2 }
                }))
                .Returns($"0|{amount1} 1|{amount2}");

            var account = new Account(mockDictionaryStringConverter.Object, null);

            account.AddOwners(_investorList);

            var expected = $"0|{amount1} 1|{amount2}";

            Assert.That(account.ShareByInvestorString, Is.EqualTo(expected));
            mockDictionaryStringConverter.VerifyAll();
        }

        [Test]
        public void ShareByInvestorString_Set_ShouldSetShareByInvestorDictionary() {
            var amounts = new float[] { 10, 20 };

            _investorList[0].Deposit(amounts[0]);
            _investorList[1].Deposit(amounts[1]);

            string stringValue = $"0|{amounts[0]} 1|{amounts[1]}";
            
            var mockDictionaryStringConverter = new Mock<IDictionaryStringConverter>();
            mockDictionaryStringConverter
                .Setup(c => c.StringToDictionary<int, float>(stringValue))
                .Returns(new Dictionary<int, float>() {
                    { 0, amounts[0] },
                    { 1, amounts[1] }
                });

            var mockInvestorService = new Mock<IDataAccess<Investor>>();
            mockInvestorService
                .Setup(s => s.GetAsync(It.IsAny<int>()))
                .Returns<int>(i => Task.FromResult(_investorList[i]));

            var account = new Account(mockDictionaryStringConverter.Object, mockInvestorService.Object);

            account.ShareByInvestorString = stringValue;

            for(int i = 0; i < account.ShareByInvestor.Count; i++) {
                Assert.That(account.ShareByInvestor.ContainsKey(_investorList[i]));
                Assert.That(account.ShareByInvestor[_investorList[i]], Is.EqualTo(amounts[i]));
            }

            mockDictionaryStringConverter.VerifyAll();
            mockInvestorService.VerifyAll();
        }

        [Test]
        public void Balance_WithShareByInvestorBeeingNull_ShouldReturnZero() {
            _emptyAccount.ShareByInvestor = null;

            Assert.That(_emptyAccount.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Balance_WithValidInvestors_ShouldReturnSumOfInvestorsBalance() {
            float amount1 = 10;
            float amount2 = 20;

            _investorList[0].Deposit(amount1);
            _investorList[1].Deposit(amount2);

            _emptyAccount.AddOwners(_investorList);

            Assert.That(_emptyAccount.Balance, Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void AddOwners_ShouldAdd() {
            Assert.That(_emptyAccount.ShareByInvestor.Count, Is.EqualTo(0));

            _emptyAccount.AddOwners(_investorList);

            Assert.That(_emptyAccount.ShareByInvestor.Count, Is.EqualTo(_investorList.Count));
        }

        [Test]
        public void AddOwner_WithExistingInvestor_ShouldNotAdd() {
            _emptyAccount.AddOwners(new List<Investor>() { _investorTester });

            _emptyAccount.AddOwner(_investorTester, 10);

            Assert.That(_emptyAccount.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_emptyAccount.ShareByInvestor.ContainsKey(_investorTester));
        }

        [Test]
        public void AddOwner_WithNewInvestor_ShouldAdd() {
            float balance = 10;

            _emptyAccount.AddOwner(_investorTester, balance);

            Assert.That(_emptyAccount.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_emptyAccount.ShareByInvestor[_investorTester], Is.EqualTo(balance));
        }

        [Test]
        public void ToList_WithValidInput_ShouldReturnKeyValuePairList() {
            _investor1.Deposit(10);
            _investor2.Deposit(20);

            var expected = new List<KeyValuePair<Investor, float>>();
            expected.Add(new KeyValuePair<Investor, float>(_investor1, _investor1.Share));
            expected.Add(new KeyValuePair<Investor, float>(_investor2, _investor2.Share));

            _emptyAccount.AddOwners(new List<Investor>() {
                _investor1,
                _investor2
            });

            var actual = _emptyAccount.ToList();

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
