using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Models;

namespace WeInvest.Tests.Models {
    class AccountTests {

        [Test]
        public void Balance_WithShareByInvestorBeeingNull_ShouldReturnZero() {
            Account account = new Account() { ShareByInvestor = null };

            Assert.That(account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Balance_WithValidInvestors_ShouldReturnSumOfInvestorsBalance() {
            float amount1 = 10;
            float amount2 = 20;

            Investor investor1 = new Investor("1", Brushes.Black);
            investor1.Deposit(amount1);
            Investor investor2 = new Investor("2", Brushes.White);
            investor2.Deposit(amount2);

            Account account = new Account(new List<Investor>() {
                investor1, 
                investor2
            });

            Assert.That(account.Balance, Is.EqualTo(amount1 + amount2));
        }

        [Test]
        public void AddOwner_WithExistingInvestor_ShouldNotAdd() {
            Investor investor = new Investor("Tester", Brushes.Black);
            Account account = new Account(new List<Investor>() { investor });

            account.AddOwner(investor, 10);

            Assert.That(account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(account.ShareByInvestor.ContainsKey(investor));
        }

        [Test]
        public void AddOwner_WithNewInvestor_ShouldAdd() {
            Account account = new Account();
            Investor investor = new Investor("Tester", Brushes.Black);
            float balance = 10;

            account.AddOwner(investor, balance);

            Assert.That(account.ShareByInvestor.Count, Is.EqualTo(1));
            Assert.That(account.ShareByInvestor[investor], Is.EqualTo(balance));
        }

        [Test]
        public void ToList_WithValidInput_ShouldReturnKeyValuePairList() {
            Investor investor1 = new Investor("1", Brushes.Black);
            investor1.Deposit(10);
            Investor investor2 = new Investor("2", Brushes.White);
            investor2.Deposit(20);

            var expected = new List<KeyValuePair<Investor, float>>();
            expected.Add(new KeyValuePair<Investor, float>(investor1, investor1.Share));
            expected.Add(new KeyValuePair<Investor, float>(investor2, investor2.Share));

            Account account = new Account(new List<Investor>() { 
                investor1,
                investor2
            });

            var actual = account.ToList();

            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
