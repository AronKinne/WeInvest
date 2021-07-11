using System;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Accounts {
    public class DisplayedAccountStore : IDisplayedAccountStore {

        private Account _displayedAccount;

        public Account DisplayedAccount {
            get => _displayedAccount;
            set {
                _displayedAccount = value;
                OnStateChanged();
            }
        }

        public event EventHandler StateChanged;

        private void OnStateChanged() {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
