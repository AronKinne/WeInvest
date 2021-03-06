using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using WeInvest.WPF.Commands;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.Views.Controls.ViewModelInterfaces;

namespace WeInvest.WPF.ViewModels.Controls {
    public class AccountsAreaChartViewModel : ViewModelBase, IAreaChartViewModel {

        private readonly IInvestorsStore _investorsStore;
        private readonly IAccountsStore _accountsStore;

        public IList<OrderedAreaData> AreaDataSeries { get; private set; } = new ObservableCollection<OrderedAreaData>();
        public IList<Brush> OrderedBrushList { get; private set; } = new ObservableCollection<Brush>();

        public ICommand SelectionChangedCommand { get; }

        public double AreaOpacity { get; set; } = 1;

        public AccountsAreaChartViewModel(IInvestorsStore investorsStore, IAccountsStore accountsStore, SetDisplayedAccountCommand setDisplayedAccountCommand) {
            _investorsStore = investorsStore;
            _investorsStore.StateChanged += InvestorsStore_StateChanged;

            _accountsStore = accountsStore;
            _accountsStore.StateChanged += AccountsStore_StateChanged;

            SelectionChangedCommand = setDisplayedAccountCommand;
        }

        private void InvestorsStore_StateChanged(object sender, System.EventArgs e) {
            OrderedBrushList = new ObservableCollection<Brush>();

            foreach(var investor in _investorsStore.Investors) {
                var brush = investor.Brush.Clone();
                brush.Opacity = AreaOpacity;
                OrderedBrushList.Add(brush);
            }

            OnPropertyChanged(nameof(OrderedBrushList));
        }

        private void AccountsStore_StateChanged(object sender, System.EventArgs e) {
            AreaDataSeries = new ObservableCollection<OrderedAreaData>();

            foreach(var account in _accountsStore.Accounts) {
                var doubleList = account.ShareByInvestor.ToList().Select(p => (double)p.Value).ToList();
                AreaDataSeries.Add(new OrderedAreaData(AreaDataSeries.Count + 1, doubleList));
            }

            OnPropertyChanged(nameof(AreaDataSeries));
        }
    }
}
