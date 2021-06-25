using NUnit.Framework;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Tests.Factories {
    public class AccountFactoryTests {

        private AccountFactory _accountFactory;

        [SetUp]
        public void SetUp() {
            _accountFactory = new AccountFactory(null, null);
        }

        [Test]
        public void Create_Always_ShouldReturnNewAccount() {
            var result = _accountFactory.Create();

            Assert.That(result is Account);
        }
    
        [Test]
        public void Create_WithId_ShouldReturnNewAccountWithProperties() {
            int id = 1;

            var result = _accountFactory.Create(new { Id = id });

            Assert.That(result.Id, Is.EqualTo(id));
        }

    }
}
