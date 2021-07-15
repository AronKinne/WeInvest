using System.Collections.ObjectModel;
using System.Windows.Input;
using WeInvest.Domain.Factories;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Navigators;
using WeInvest.WPF.State.Stocks;

namespace WeInvest.WPF.ViewModels.Controls {
    public class TabBarViewModel : ViewModelBase {

        private readonly INavigator _navigator;
        private readonly IStocksStore _stocksStore;
        private readonly IFactory<StockViewModel> _stockViewModelFactory;

        public ObservableCollection<StockViewModel> StockViewModels { get; private set; } = new ObservableCollection<StockViewModel>();
        public ICommand NavigateCommand { get; }
        public ViewModelBase HomeViewModel { get; }

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public TabBarViewModel(INavigator navigator, HomeViewModel homeViewModel, IStocksStore stocksStore, IFactory<StockViewModel> stockViewModelFactory) {
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;
            _stocksStore = stocksStore;
            _stocksStore.StateChanged += StocksStore_StateChanged;
            _stockViewModelFactory = stockViewModelFactory;

            NavigateCommand = new NavigateCommand(_navigator);
            HomeViewModel = homeViewModel;
        }

        private void Navigator_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void StocksStore_StateChanged(object sender, System.EventArgs e) {
            StockViewModels = new ObservableCollection<StockViewModel>();

            foreach(var stock in _stocksStore.Stocks)
                StockViewModels.Add(_stockViewModelFactory.Create(stock));

            OnPropertyChanged(nameof(StockViewModels));
        }
    }
}
