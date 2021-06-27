using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.DataAccess {
    public class InvestorDataAccess : GenericDataAccess<Investor> {

        private readonly IFactory<Investor> _investorFactory;

        public InvestorDataAccess(IFactory<IDbConnection> connectionFactory, IQueryService queryService, IFactory<Investor> investorFactory) : base(
            connectionFactory,
            queryService,
            "SQLite",
            "Investor",
            new List<PropertyInfo>() {
                typeof(Investor).GetProperty(nameof(Investor.Name)),
                typeof(Investor).GetProperty(nameof(Investor.ColorHex)),
                typeof(Investor).GetProperty(nameof(Investor.ShareHistoryString))
            }) {

            _investorFactory = investorFactory;
        }

        protected override Investor CreateEntity(dynamic dynamicObject) {
            return _investorFactory.Create(new {
                Id = dynamicObject.Id,
                Name = dynamicObject.Name,
                ColorHex = dynamicObject.ColorHex,
                ShareHistoryString = dynamicObject.ShareHistoryString
            });
        }

    }
}
