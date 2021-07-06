using System;
using System.Windows.Input;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Services;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands {
    public class AddInvestorCommand : ICommand {

        private readonly MainWindowViewModel _viewModel;
        private readonly IDialogService<InvestorDialog, InvestorDialogViewModel> _investorDialogService;
        private readonly IFactory<Investor> _investorFactory;
        private readonly IDataAccess<Investor> _investorDataAccess;

        public event EventHandler CanExecuteChanged;

        public AddInvestorCommand(MainWindowViewModel viewModel, IDialogService<InvestorDialog, InvestorDialogViewModel> investorDialogService, IFactory<Investor> investorFactory, IDataAccess<Investor> investorDataAccess) {
            _viewModel = viewModel;
            _investorDialogService = investorDialogService;
            _investorFactory = investorFactory;
            _investorDataAccess = investorDataAccess;
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

                var createdInvestor = await _investorDataAccess.CreateAsync(investor);
                _viewModel.InvestorGroup.AddInvestor(createdInvestor);
            }
        }

    }
}
