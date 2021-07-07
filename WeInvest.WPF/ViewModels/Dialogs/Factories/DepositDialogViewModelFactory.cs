using WeInvest.Domain.Factories;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Dialogs.Factories {
    public class DepositDialogViewModelFactory : IFactory<DepositDialogViewModel> {

        private readonly IInvestorsStore _investorsStore;

        public DepositDialogViewModelFactory(IInvestorsStore investorsStore) {
            _investorsStore = investorsStore;
        }

        public DepositDialogViewModel Create() {
            return new DepositDialogViewModel(_investorsStore);
        }

        public DepositDialogViewModel Create(object parameter) {
            return Create();
        }
    }
}
