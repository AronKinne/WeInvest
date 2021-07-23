using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.Views.Controls.ViewModelInterfaces;

namespace WeInvest.WPF.ViewModels.Controls {
    public class DisplayedAccountPieChartViewModel : ViewModelBase, IPieChartViewModel {

        private readonly IAccountsStore _accountsStore;
        private readonly IDisplayedAccountStore _displayedAccountStore;

        public IList<PieData> PieSeries { get; private set; } = new ObservableCollection<PieData>();

        public DisplayedAccountPieChartViewModel(IAccountsStore accountsStore, IDisplayedAccountStore displayedAccountStore) {
            _accountsStore = accountsStore;
            _accountsStore.StateChanged += AccountsStore_StateChanged;

            _displayedAccountStore = displayedAccountStore;
            _displayedAccountStore.StateChanged += DisplayedAccountStore_StateChanged;
        }

        private void AccountsStore_StateChanged(object sender, EventArgs e) {
            _displayedAccountStore.DisplayedAccount = _accountsStore.Last;
        }

        private void DisplayedAccountStore_StateChanged(object sender, EventArgs e) {
            if(_displayedAccountStore.DisplayedAccount == null)
                return;

            PieSeries = new ObservableCollection<PieData>();
            var accountList = _displayedAccountStore.DisplayedAccount.ShareByInvestor.ToList();
            foreach(var entry in accountList)
                PieSeries.Add(new PieData(entry.Key.Brush, entry.Value));

            OnPropertyChanged(nameof(PieSeries));
        }

    }
}
