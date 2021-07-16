using System;
using System.Collections.Generic;
using System.Windows.Input;
using WeInvest.Domain.Models;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Dialogs {
    public class DepositDialogViewModel : DialogViewModelBase {

        private readonly IInvestorsStore _investorsStore;

        public Investor SelectedInvestor { get; set; }
        public IEnumerable<Investor> InvestorPool => _investorsStore.Investors;

        private float _amount;

        public float Amount {
            get { return _amount; }
            set { _amount = Math.Abs(value); }
        }


        public DepositDialogViewModel(IInvestorsStore investorsStore) {
            _investorsStore = investorsStore;

            OkayButtonContent = "Deposit";
        }

        protected override bool CanOkay(object parameter) {
            return SelectedInvestor != null && Amount > 0;
        }

    }
}
