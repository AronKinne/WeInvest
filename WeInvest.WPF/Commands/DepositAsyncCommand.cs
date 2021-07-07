using System;
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
    public class DepositAsyncCommand : AsyncCommandBase {

        private readonly IInvestorsStore _investorsStore;
        private readonly IAccountsStore _accountsStore;
        private readonly DialogServiceFactory<DepositDialog, DepositDialogViewModel> _dialogServiceFactory;
        private readonly ITransactionService _transactionService;
        private readonly IFactory<Account> _accountFactory;
        private readonly IDataAccess<Account> _accountDataAccess;

        public DepositAsyncCommand(IInvestorsStore investorsStore, IAccountsStore accountsStore, DialogServiceFactory<DepositDialog, DepositDialogViewModel> dialogServiceFactory, ITransactionService transactionService, IFactory<Account> accountFactory, IDataAccess<Account> accountDataAccess) {
            _investorsStore = investorsStore;
            _accountsStore = accountsStore;
            _dialogServiceFactory = dialogServiceFactory;
            _transactionService = transactionService;
            _accountFactory = accountFactory;
            _accountDataAccess = accountDataAccess;
        }

        protected override async Task ExecuteAsync(object parameter) {
            var dialogService = _dialogServiceFactory.CreateAndInitialize();
            if(dialogService.ShowDialog() == true) {

            }

            Random random = new Random();

            var randomIndex = random.Next(_investorsStore.CurrentInvestors.Count);
            var selectedInvestor = _investorsStore.CurrentInvestors[randomIndex];
            var amount = random.Next(20, 50);
            var updatedInvestor = await Task.Run(() => _transactionService.DepositAsync(selectedInvestor, amount));
            _investorsStore.CurrentInvestors[randomIndex] = updatedInvestor;

            var account = _accountFactory.Create();
            account.AddOwners(_investorsStore.CurrentInvestors);
            var dbAccount = await Task.Run(() => _accountDataAccess.CreateAsync(account));
            _accountsStore.CurrentAccounts.Add(dbAccount);
        }
    }
}
