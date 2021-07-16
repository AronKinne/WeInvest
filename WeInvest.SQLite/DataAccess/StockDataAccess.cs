using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.DataAccess {
    public class StockDataAccess : GenericDataAccess<Stock> {

        private readonly IFactory<Stock> _stockFactory;

        public StockDataAccess(IFactory<IDbConnection> connectionFactory, IQueryService queryService, IFactory<Stock> stockFactory) : base(
            connectionFactory,
            queryService,
            "SQLite",
            "Stock",
            new List<PropertyInfo>() {
                typeof(Stock).GetProperty(nameof(Stock.Symbol)),
                typeof(Stock).GetProperty(nameof(Stock.Name))
            }) {

            _stockFactory = stockFactory;
        }

        protected override Stock CreateEntity(dynamic dynamicObject) {
            return _stockFactory.Create(new {
                Id = dynamicObject.Id,
                Symbol = dynamicObject.Symbol,
                Name = dynamicObject.Name
            });
        }

    }
}
