using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands {
    public class AddInvestorAsyncCommand : AsyncCommandBase {

        private readonly IInvestorsStore _investorsStore;
        private readonly DialogServiceFactory<InvestorDialog, InvestorDialogViewModel> _dialogServiceFactory;
        private readonly IFactory<Investor> _investorFactory;
        private readonly IDataAccess<Investor> _investorDataAccess;
        private readonly IAccountsStore _accountsStore;
        private readonly IDataAccess<Account> _accountDataAccess;

        public AddInvestorAsyncCommand(IInvestorsStore investorsStore, DialogServiceFactory<InvestorDialog, InvestorDialogViewModel> dialogServiceFactory, IFactory<Investor> investorFactory, IDataAccess<Investor> investorDataAccess, IAccountsStore accountsStore, IDataAccess<Account> accountDataAccess) {
            _investorsStore = investorsStore;
            _dialogServiceFactory = dialogServiceFactory;
            _investorFactory = investorFactory;
            _investorDataAccess = investorDataAccess;
            _accountsStore = accountsStore;
            _accountDataAccess = accountDataAccess;
        }

        protected override async Task ExecuteAsync(object parameter) {
            var dialogService = _dialogServiceFactory.CreateAndInitialize();
            if(dialogService.ShowDialog() == true) {
                var viewModel = dialogService.ViewModel;

                var investor = _investorFactory.Create();
                investor.Name = viewModel.InvestorName;
                investor.Brush = viewModel.InvestorBrush;

                var dbInvestor = await Task.Run(() => _investorDataAccess.CreateAsync(investor));
                _investorsStore.Investors.Add(dbInvestor);

                var updatedAccounts = new ObservableCollection<Account>();
                foreach(var account in _accountsStore.Accounts) {
                    account.AddOwner(dbInvestor, 0);
                    await Task.Run(() => _accountDataAccess.UpdateAsync(account.Id, account));
                    updatedAccounts.Add(account);
                }
                _accountsStore.Accounts = updatedAccounts;
            }
        }

    }
}
