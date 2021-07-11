using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Accounts {
    public class AccountsStore : IAccountsStore {

        private ObservableCollection<Account> _accounts;

        public event EventHandler StateChanged;

        public ObservableCollection<Account> Accounts {
            get => _accounts;
            set {
                _accounts = value;
                OnStateChanged(EventArgs.Empty);
                Accounts.CollectionChanged += Accounts_CollectionChanged;
            }
        }

        private void Accounts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnStateChanged(e);
        }

        private void OnStateChanged(EventArgs e) {
            StateChanged?.Invoke(this, e);
        }

    }
}
