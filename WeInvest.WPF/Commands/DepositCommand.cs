using System;
using System.Windows.Input;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands {
    public class DepositCommand : ICommand {

        private readonly IInvestorsStore _investorsStore;
        private readonly IAccountsStore _accountsStore;
        private readonly IDialogService<DepositDialog, DepositDialogViewModel> _dialogService;
        private readonly ITransactionService _transactionService;
        private readonly IFactory<Account> _accountFactory;
        private readonly IDataAccess<Account> _accountDataAccess;

        public DepositCommand(IInvestorsStore investorsStore, IAccountsStore accountsStore, IDialogService<DepositDialog, DepositDialogViewModel> dialogService, ITransactionService transactionService, IFactory<Account> accountFactory, IDataAccess<Account> accountDataAccess) {
            _investorsStore = investorsStore;
            _accountsStore = accountsStore;
            _dialogService = dialogService;
            _transactionService = transactionService;
            _accountFactory = accountFactory;
            _accountDataAccess = accountDataAccess;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            //if(_dialogService.ShowDialog() == true) {

            //}

            Random random = new Random();

            var randomIndex = random.Next(_investorsStore.CurrentInvestors.Count);
            var selectedInvestor = _investorsStore.CurrentInvestors[randomIndex];
            var amount = random.Next(20, 50);
            var updatedInvestor = await _transactionService.DepositAsync(selectedInvestor, amount);
            _investorsStore.CurrentInvestors[randomIndex] = updatedInvestor;

            var account = _accountFactory.Create();
            account.AddOwners(_investorsStore.CurrentInvestors);
            var dbAccount = await _accountDataAccess.CreateAsync(account);
            _accountsStore.CurrentAccounts.Add(dbAccount);
        }
    }
}
