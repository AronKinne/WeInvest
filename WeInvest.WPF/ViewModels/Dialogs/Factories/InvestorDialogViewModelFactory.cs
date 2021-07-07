using WeInvest.Domain.Factories;

namespace WeInvest.WPF.ViewModels.Dialogs.Factories {
    public class InvestorDialogViewModelFactory : IFactory<InvestorDialogViewModel> {
        public InvestorDialogViewModel Create() {
            return new InvestorDialogViewModel();
        }

        public InvestorDialogViewModel Create(object parameter) {
            return Create();
        }
    }
}
