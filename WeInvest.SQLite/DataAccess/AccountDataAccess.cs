using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.DataAccess {
    public class AccountDataAccess : GenericDataAccess<Account> {

        private readonly IFactory<Account> _accountFactory;

        public AccountDataAccess(IFactory<IDbConnection> connectionFactory, IQueryService queryService, IFactory<Account> accountFactory) : base(
            connectionFactory,
            queryService,
            "SQLite",
            "Account",
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
