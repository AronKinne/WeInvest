using WeInvest.Domain.Models;

namespace WeInvest.WPF.ViewModels {
    public class StockViewModel : ViewModelBase {

        private readonly Stock _stock;

        public string StockSymbol => _stock.Symbol;
        public string StockName => _stock.Name;

        public StockViewModel(Stock stock) {
            _stock = stock;
        }

    }
}
