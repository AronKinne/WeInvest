using System;
using System.Collections.ObjectModel;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Stocks {
    public class StocksStore : IStocksStore {

        private ObservableCollection<Stock> _stocks;
        public ObservableCollection<Stock> Stocks {
            get => _stocks;
            set {
                _stocks = value;
                OnStateChanged();
            }
        }

        public event EventHandler StateChanged;

        private void OnStateChanged() {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
