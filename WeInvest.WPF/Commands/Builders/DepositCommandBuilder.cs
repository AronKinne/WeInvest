using WeInvest.WPF.Services;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Commands.Builders {
    public class DepositCommandBuilder : IBuilder<DepositCommand> {

        private DepositCommand _depositCommand;

        private readonly MainWindowViewModel _viewModel;
        private readonly IDialogService<DepositDialog, DepositDialogViewModel> _dialogService;

        public DepositCommandBuilder(MainWindowViewModel viewModel, IDialogService<DepositDialog, DepositDialogViewModel> dialogService) {
            _viewModel = viewModel;
            _dialogService = dialogService;
        }

        public IBuilder<DepositCommand> Build() {
            _depositCommand = new DepositCommand(_viewModel, _dialogService);
            return this;
        }

        public DepositCommand Get() {
            return _depositCommand;
        }
    }
}
