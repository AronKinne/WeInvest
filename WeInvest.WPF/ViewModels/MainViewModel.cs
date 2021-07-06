using System.Collections.ObjectModel;
using System.Windows.Input;
using WeInvest.Domain.Models;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.ViewModels.Controls;

namespace WeInvest.WPF.ViewModels {
    public class MainViewModel : ViewModelBase {

        private readonly IInvestorsStore _investorsStore;

        public ObservableCollection<Investor> Investors => _investorsStore.CurrentInvestors;
        public ICommand DepositCommand { get; }
        public ICommand AddInvestorCommand { get; }

        public MainAccountPieControlViewModel MainAccountPieViewModel { get; set; }
        public MainAccountAreaControlViewModel MainAccountAreaViewModel { get; set; }
        public InvestorChartControlViewModel InvestorChartViewModel { get; set; }

        public MainViewModel(IInvestorsStore investorsStore, IAccountsStore accountsStore, DepositCommand depositCommand, AddInvestorCommand addInvestorCommand) {
            _investorsStore = investorsStore;
            _investorsStore.StateChanged += _investorsStore_StateChanged;

            DepositCommand = depositCommand;
            AddInvestorCommand = addInvestorCommand;

            MainAccountPieViewModel = new MainAccountPieControlViewModel(accountsStore);
            MainAccountAreaViewModel = new MainAccountAreaControlViewModel(_investorsStore, accountsStore) { AreaOpacity = 1 };
            InvestorChartViewModel = new InvestorChartControlViewModel(_investorsStore);
        }

        private void _investorsStore_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(Investors));
        }
    }
}
