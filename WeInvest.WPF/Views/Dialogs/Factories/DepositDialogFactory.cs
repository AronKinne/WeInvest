using WeInvest.Domain.Factories;

namespace WeInvest.WPF.Views.Dialogs.Factories {
    public class DepositDialogFactory : IFactory<DepositDialog> {
        public DepositDialog Create() {
            return new DepositDialog();
        }

        public DepositDialog Create(object parameter) {
            return Create();
        }
    }
}
