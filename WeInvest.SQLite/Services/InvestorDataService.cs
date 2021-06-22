using System.Collections.Generic;
using System.Reflection;
using WeInvest.Domain.Models;

namespace WeInvest.SQLite.Services {
    public class InvestorDataService : GenericDataService<Investor> {

        public InvestorDataService() : base(
            "Investor",
            "SQLite",
            new List<PropertyInfo>() {
                typeof(Investor).GetProperty(nameof(Investor.Name)),
                typeof(Investor).GetProperty(nameof(Investor.ColorHex)),
                typeof(Investor).GetProperty(nameof(Investor.ShareHistoryString))
            }) { }

    }
}
