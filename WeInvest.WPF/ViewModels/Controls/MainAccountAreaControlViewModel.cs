using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Controls {
    public class MainAccountAreaControlViewModel : ViewModelBase {

        public IInvestorsStore InvestorsStore { get; }
        public IAccountsStore AccountsStore { get; }

        public IList<OrderedAreaData> AreaDataSeries { get; set; } = new ObservableCollection<OrderedAreaData>();
        public IList<Brush> BrushList { get; set; } = new ObservableCollection<Brush>();

        public double AreaOpacity { get; set; } = 1;

        public MainAccountAreaControlViewModel(IInvestorsStore investorsStore, IAccountsStore accountsStore) {
            InvestorsStore = investorsStore;
            InvestorsStore.StateChanged += InvestorsStore_StateChanged;

            AccountsStore = accountsStore;
            AccountsStore.StateChanged += AccountsStore_StateChanged;

            BrushList.Add(Brushes.LightCoral);
            BrushList.Add(Brushes.LightSalmon);
            BrushList.Add(Brushes.Aquamarine);
        }

        private void InvestorsStore_StateChanged(object sender, System.EventArgs e) {
            UpdateBrushList();
        }

        private void AccountsStore_StateChanged(object sender, System.EventArgs e) {
            UpdateAreaDataSeries();
        }

        private void UpdateAreaDataSeries() {
            AreaDataSeries = new ObservableCollection<OrderedAreaData>();

            foreach(var account in AccountsStore.Accounts) {
                var doubleList = account.ShareByInvestor.ToList().Select(p => (double)p.Value).ToList();
                AreaDataSeries.Add(new OrderedAreaData(AreaDataSeries.Count + 1, doubleList));
            }

            OnPropertyChanged(nameof(AreaDataSeries));
        }

        private void UpdateBrushList() {
            BrushList = new ObservableCollection<Brush>();

            foreach(var investor in InvestorsStore.Investors) {
                var brush = investor.Brush.Clone();
                brush.Opacity = AreaOpacity;
                BrushList.Add(brush);
            }

            OnPropertyChanged(nameof(BrushList));
        }
    }
}
