using WeInvest.Domain.Models;
using WeInvest.WPF.ViewModels.Controls;

namespace WeInvest.WPF.ViewModels {
    public class StockViewModel : ViewModelBase {

        private readonly Stock _stock;

        public string StockSymbol => _stock.Symbol;
        public string StockName => _stock.Name;

        public StockPieChartViewModel StockPieChartViewModel { get; private set; }
        public StockAreaChartViewModel StockAreaChartViewModel { get; private set; }

        public StockViewModel(Stock stock) {
            _stock = stock;

            StockPieChartViewModel = new StockPieChartViewModel();
            StockAreaChartViewModel = new StockAreaChartViewModel();
        }

    }
}
