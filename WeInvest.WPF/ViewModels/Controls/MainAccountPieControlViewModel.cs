using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeInvest.Domain.Models;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Accounts;

namespace WeInvest.WPF.ViewModels.Controls {
    public class MainAccountPieControlViewModel : ViewModelBase {

        private readonly IAccountsStore _accountsStore;
        private readonly IDisplayedAccountStore _displayedAccountStore;

        private int _displayedAccountIndex;

        public int MaxAccountIndex { get => _accountsStore.Accounts?.Count - 1 ?? -1; }
        public int DisplayedAccountIndex {
            get => _displayedAccountIndex;
            set {
                _displayedAccountIndex = Math.Max(0, Math.Min(value, _accountsStore.Accounts.Count - 1));
                if(_accountsStore.Accounts?.Count > 0)
                    _displayedAccountStore.DisplayedAccount = _accountsStore.Accounts[DisplayedAccountIndex];
                OnPropertyChanged();
            }
        }
        public Account DisplayedAccount => _displayedAccountStore.DisplayedAccount;
        public IList<PieData> PieSeries { get; set; } = new ObservableCollection<PieData>();

        public MainAccountPieControlViewModel(IAccountsStore accountsStore, IDisplayedAccountStore displayedAccountStore) {
            _accountsStore = accountsStore;
            _accountsStore.StateChanged += AccountsStore_StateChanged;

            _displayedAccountStore = displayedAccountStore;
            _displayedAccountStore.StateChanged += DisplayedAccountStore_StateChanged;
        }

        private void AccountsStore_StateChanged(object sender, EventArgs e) {
            OnPropertyChanged(nameof(MaxAccountIndex));
            DisplayedAccountIndex = MaxAccountIndex;
        }

        private void DisplayedAccountStore_StateChanged(object sender, EventArgs e) {
            UpdatePieSeries();
        }

        private void UpdatePieSeries() {
            if(DisplayedAccount == null)
                return;

            this.PieSeries = new ObservableCollection<PieData>();
            var accountList = DisplayedAccount.ShareByInvestor.ToList();
            foreach(var entry in accountList)
                PieSeries.Add(new PieData(entry.Key.Brush, entry.Value));

            OnPropertyChanged(nameof(PieSeries));
        }

    }
}
