using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeInvest.Domain.Models;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Accounts;

namespace WeInvest.WPF.ViewModels.Controls {
    public class MainAccountPieControlViewModel : ViewModelBase {

        private int _displayedAccountIndex;
        private Account _displayedAccount;

        public IAccountsStore AccountsStore { get; }

        public int MaxAccountIndex { get => AccountsStore.CurrentAccounts?.Count - 1 ?? -1; }
        public int DisplayedAccountIndex {
            get => _displayedAccountIndex;
            set {
                _displayedAccountIndex = Math.Max(0, Math.Min(value, AccountsStore.CurrentAccounts.Count - 1));
                if(AccountsStore.CurrentAccounts?.Count > 0)
                    DisplayedAccount = AccountsStore.CurrentAccounts[DisplayedAccountIndex];
                OnPropertyChanged();
            }
        }
        public Account DisplayedAccount {
            get => _displayedAccount;
            set {
                _displayedAccount = value;
                UpdatePieSeries();
            }
        }
        public IList<PieData> PieSeries { get; set; } = new ObservableCollection<PieData>();

        public MainAccountPieControlViewModel(IAccountsStore accountsStore) {
            AccountsStore = accountsStore;
            AccountsStore.StateChanged += AccountsStore_StateChanged;
        }

        private void AccountsStore_StateChanged(object sender, EventArgs e) {
            OnPropertyChanged(nameof(MaxAccountIndex));
            DisplayedAccountIndex = MaxAccountIndex;
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
