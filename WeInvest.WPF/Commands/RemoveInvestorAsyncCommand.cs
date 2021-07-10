using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.Commands {
    public class RemoveInvestorAsyncCommand : AsyncCommandBase {

        private readonly IDataAccess<Investor> _investorDataAccess;
        private readonly IDataAccess<Account> _accountDataAccess;
        private readonly IInvestorsStore _investorsStore;
        private readonly IAccountsStore _accountsStore;

        public RemoveInvestorAsyncCommand(IDataAccess<Investor> investorDataAccess, IDataAccess<Account> accountDataAccess, IInvestorsStore investorsStore, IAccountsStore accountsStore) {
            _investorDataAccess = investorDataAccess;
            _accountDataAccess = accountDataAccess;
            _investorsStore = investorsStore;
            _accountsStore = accountsStore;
        }

        protected override async Task ExecuteAsync(object parameter) {
            var investor = parameter as Investor;

            var updatedAccounts = new ObservableCollection<Account>();
            foreach(var account in _accountsStore.CurrentAccounts) {
                account.RemoveOwner(investor.Id);
                await Task.Run(() => _accountDataAccess.UpdateAsync(account.Id, account));
                updatedAccounts.Add(account);
            }
            _accountsStore.CurrentAccounts = updatedAccounts;

            _investorsStore.CurrentInvestors.Remove(investor);
            await _investorDataAccess.DeleteAsync(investor.Id);
        }

    }
}
