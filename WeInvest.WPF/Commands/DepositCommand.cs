using System;
using System.Windows.Input;
using WeInvest.WPF.Services;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands {
    public class DepositCommand : ICommand {

        private readonly MainWindowViewModel _viewModel;
        private readonly IDialogService<DepositDialog, DepositDialogViewModel> _dialogService;

        public DepositCommand(MainWindowViewModel viewModel, IDialogService<DepositDialog, DepositDialogViewModel> dialogService) {
            _viewModel = viewModel;
            _dialogService = dialogService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if(_dialogService.ShowDialog() == true) {

            }

            Random random = new Random();
            _viewModel.InvestorGroup.Deposit(_viewModel.Investors[random.Next(_viewModel.Investors.Count)], random.Next(20, 50));
            _viewModel.MainAccountPieViewModel.DisplayedAccountIndex = _viewModel.InvestorGroup.AccountHistory.Count - 1;
        }
    }
}
