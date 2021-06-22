using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.WPF.Utilities;

namespace WeInvest.Domain.Tests.Models {
    public class AccountTests {

        private InvestorFactory _investorFactory;

        private Account _account;
        private Investor _investorTester, _investor1, _investor2;
        private IList<Investor> _investorList;

        [SetUp]
        public void SetUp() {
            var serviceProvider = ServiceProviderFactory.Create();
            _investorFactory = serviceProvider.GetRequiredService<IFactory<Investor>>() as InvestorFactory;

            _account = new Account();

            _investorTester = _investorFactory.Create("Tester", Brushes.Black);
            _investor1 = _investorFactory.Create("1", Brushes.Black);
            _investor2 = _investorFactory.Create("2", Brushes.White);

            _investorList = new List<Investor>() { _investor1, _investor2 };
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

            _investorList[0].Deposit(amount1);
            _investorList[1].Deposit(amount2);

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
        public void ToList_WithValidInput_ShouldReturnKeyValuePairList() {
            _investor1.Deposit(10);
            _investor2.Deposit(20);

            var expected = new List<KeyValuePair<Investor, float>>();
            expected.Add(new KeyValuePair<Investor, float>(_investor1, _investor1.Share));
            expected.Add(new KeyValuePair<Investor, float>(_investor2, _investor2.Share));

            _account.AddOwners(new List<Investor>() {
                _investor1,
                _investor2
            });

            var actual = _account.ToList();

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
