using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Accounts {
    public class AccountsStore : IAccountsStore {

        private ObservableCollection<Account> _currentAccounts;

        public event EventHandler StateChanged;

        public ObservableCollection<Account> CurrentAccounts {
            get => _currentAccounts;
            set {
                _currentAccounts = value;
                OnStateChanged(EventArgs.Empty);
                CurrentAccounts.CollectionChanged += CurrentAccounts_CollectionChanged;
            }
        }

        private void CurrentAccounts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnStateChanged(e);
        }

        private void OnStateChanged(EventArgs e) {
            StateChanged?.Invoke(this, e);
        }

    }
}
