using System;
using System.Windows.Input;
using WeInvest.WPF.State.Accounts;

namespace WeInvest.WPF.Commands {
    public class SetDisplayedAccountCommand : ICommand {

        private readonly IAccountsStore _accountsStore;
        private readonly IDisplayedAccountStore _displayedAccountStore;

        public SetDisplayedAccountCommand(IAccountsStore accountsStore, IDisplayedAccountStore displayedAccountStore) {
            _accountsStore = accountsStore;
            _displayedAccountStore = displayedAccountStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            int index = (int)parameter;
            if(index < 0) {
                _displayedAccountStore.DisplayedAccount = _accountsStore.Last;
                return;
            }

            _displayedAccountStore.DisplayedAccount = _accountsStore.Accounts[index];
        }

    }
}
