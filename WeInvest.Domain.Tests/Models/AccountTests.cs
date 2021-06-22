using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Models {
    public class AccountTests {

        private Account _account;
        private Investor _investor;
        private IList<Investor> _investorList;

        [SetUp]
        public void SetUp() {
            _account = new Account();

            _investor = new Investor() { Name = "Tester", Brush = Brushes.Black };

            _investorList = new List<Investor>() {
                new Investor() { Name = "1", Brush = Brushes.Black },
                new Investor() { Name = "2", Brush = Brushes.White }
            };
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
            _account.AddOwners(new List<Investor>() { _investor });

            _account.AddOwner(_investor, 10);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_account.ShareByInvestor.ContainsKey(_investor));
        }

        [Test]
        public void AddOwner_WithNewInvestor_ShouldAdd() {
            float balance = 10;

            _account.AddOwner(_investor, balance);

            Assert.That(_account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(_account.ShareByInvestor[_investor], Is.EqualTo(balance));
        }

        [Test]
        public void ToList_WithValidInput_ShouldReturnKeyValuePairList() {
            Investor investor1 = new Investor() { Name = "1", Brush = Brushes.Black };
            investor1.Deposit(10);
            Investor investor2 = new Investor() { Name = "2", Brush = Brushes.White };
            investor2.Deposit(20);

            var expected = new List<KeyValuePair<Investor, float>>();
            expected.Add(new KeyValuePair<Investor, float>(investor1, investor1.Share));
            expected.Add(new KeyValuePair<Investor, float>(investor2, investor2.Share));

            _account.AddOwners(new List<Investor>() {
                investor1,
                investor2
            });

            var actual = _account.ToList();

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
