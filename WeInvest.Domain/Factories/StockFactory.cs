using WeInvest.Domain.Models;

namespace WeInvest.Domain.Factories {
    public class StockFactory : IFactory<Stock> {

        public Stock Create() {
            return new Stock();
        }

        public Stock Create(object parameter) {
            Stock stock = Create();

            stock.Id = parameter.GetProperty<int>(nameof(Stock.Id));
            stock.Symbol = parameter.GetProperty<string>(nameof(Stock.Symbol));
            stock.Name = parameter.GetProperty<string>(nameof(Stock.Name));

            return stock;
        }
    }
}
