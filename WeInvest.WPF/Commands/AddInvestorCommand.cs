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
    public class AddInvestorCommand : ICommand {

        private readonly IInvestorsStore _investorsStore;
        private readonly IDialogService<InvestorDialog, InvestorDialogViewModel> _investorDialogService;
        private readonly IFactory<Investor> _investorFactory;
        private readonly IDataAccess<Investor> _investorDataAccess;
        private readonly IAccountsStore _accountsStore;
        private readonly IDataAccess<Account> _accountDataAccess;

        public event EventHandler CanExecuteChanged;

        public AddInvestorCommand(IInvestorsStore investorsStore, IDialogService<InvestorDialog, InvestorDialogViewModel> investorDialogService, IFactory<Investor> investorFactory, IDataAccess<Investor> investorDataAccess, IAccountsStore accountsStore, IDataAccess<Account> accountDataAccess) {
            _investorsStore = investorsStore;
            _investorDialogService = investorDialogService;
            _investorFactory = investorFactory;
            _investorDataAccess = investorDataAccess;
            _accountsStore = accountsStore;
            _accountDataAccess = accountDataAccess;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            if(_investorDialogService.ShowDialog() == true) {
                var viewModel = _investorDialogService.ViewModel;

                var investor = _investorFactory.Create();
                investor.Name = viewModel.InvestorName;
                investor.Brush = viewModel.InvestorBrush;

                var dbInvestor = await _investorDataAccess.CreateAsync(investor);
                _investorsStore.CurrentInvestors.Add(dbInvestor);

                foreach(var account in _accountsStore.CurrentAccounts) {
                    account.AddOwner(dbInvestor, 0);
                    await _accountDataAccess.UpdateAsync(account.Id, account);
                }
            }
        }

    }
}
