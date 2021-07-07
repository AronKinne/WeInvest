using WeInvest.Domain.Factories;

namespace WeInvest.WPF.ViewModels.Dialogs.Factories {
    public class DepositDialogViewModelFactory : IFactory<DepositDialogViewModel> {
        public DepositDialogViewModel Create() {
            return new DepositDialogViewModel();
        }

        public DepositDialogViewModel Create(object parameter) {
            return Create();
        }
    }
}
