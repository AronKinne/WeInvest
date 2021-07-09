using System.Collections.Generic;
using WeInvest.Domain.Models;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Dialogs {
    public class DepositDialogViewModel : DialogViewModelBase {

        private readonly IInvestorsStore _investorsStore;

        public Investor SelectedInvestor { get; set; }
        public IEnumerable<Investor> InvestorPool => _investorsStore.CurrentInvestors;
        public float Amount { get; set; }

        public DepositDialogViewModel(IInvestorsStore investorsStore) {
            _investorsStore = investorsStore;

            OkayButtonContent = "Deposit";
        }

    }
}
