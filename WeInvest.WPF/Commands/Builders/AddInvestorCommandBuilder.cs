using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Services;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands.Builders {
    public class AddInvestorCommandBuilder : IBuilder<AddInvestorCommand> {

        private AddInvestorCommand _command;

        private readonly MainWindowViewModel _viewModel;
        private readonly IDialogService<InvestorDialog, InvestorDialogViewModel> _investorDialogService;
        private readonly IFactory<Investor> _investorFactory;
        private readonly IDataAccess<Investor> _investorDataAccess;

        public AddInvestorCommandBuilder(MainWindowViewModel viewModel, IDialogService<InvestorDialog, InvestorDialogViewModel> investorDialogService, IFactory<Investor> investorFactory, IDataAccess<Investor> investorDataAccess) {
            _viewModel = viewModel;
            _investorDialogService = investorDialogService;
            _investorFactory = investorFactory;
            _investorDataAccess = investorDataAccess;
        }

        public IBuilder<AddInvestorCommand> Build() {
            _command = new AddInvestorCommand(_viewModel, _investorDialogService, _investorFactory, _investorDataAccess);
            return this;
        }

        public AddInvestorCommand Get() {
            return _command;
        }

    }
}
