using System.Collections.Generic;
using System.Reflection;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.SQLite.Services {
    public class AccountDataService : GenericDataService<Account> {

        private readonly IFactory<Account> _accountFactory;

        public AccountDataService(IFactory<Account> accountFactory) : base(
            "Account",
            "SQLite",
            new List<PropertyInfo>() {
                typeof(Account).GetProperty(nameof(Account.ShareByInvestorString))
            }) {

            _accountFactory = accountFactory;

        }

        protected override Account CreateEntity(dynamic dynamicObject) {
            return _accountFactory.Create(new {
                Id = dynamicObject.Id,
                ShareByInvestorString = dynamicObject.ShareByInvestorString
            });
        }
    }
}
