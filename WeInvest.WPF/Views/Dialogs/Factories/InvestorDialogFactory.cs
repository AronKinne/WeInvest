using WeInvest.Domain.Factories;

namespace WeInvest.WPF.Views.Dialogs.Factories {
    public class InvestorDialogFactory : IFactory<InvestorDialog> {
        public InvestorDialog Create() {
            return new InvestorDialog();
        }

        public InvestorDialog Create(object parameter) {
            return Create();
        }
    }
}
