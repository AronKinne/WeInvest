using Dapper;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;

namespace WeInvest.SQLite.Services {
    public class InvestorDataService : GenericDataService<Investor> {

        private readonly IFactory<Investor> _investorFactory;

        public InvestorDataService(IFactory<Investor> investorFactory) : base(
            "Investor",
            "SQLite",
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
