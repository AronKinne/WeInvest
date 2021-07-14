using System.Collections.ObjectModel;
using System.Windows.Input;
using WeInvest.Domain.Models;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.ViewModels.Controls;

namespace WeInvest.WPF.ViewModels {
    public class HomeViewModel : ViewModelBase {

        private readonly IInvestorsStore _investorsStore;

        public ObservableCollection<Investor> Investors => _investorsStore.Investors;
        public ICommand DepositCommand { get; }
        public ICommand AddInvestorCommand { get; }
        public ICommand RemoveInvestorCommand { get; }

        public DisplayedAccountPieChartViewModel MainAccountPieViewModel { get; }
        public AccountsAreaChartViewModel MainAccountAreaViewModel { get; }
        public InvestorLineChartsViewModel InvestorChartViewModel { get; }

        public HomeViewModel(IInvestorsStore investorsStore, DepositAsyncCommand depositAsyncCommand, AddInvestorAsyncCommand addInvestorAsyncCommand, RemoveInvestorAsyncCommand removeInvestorAsyncCommand, DisplayedAccountPieChartViewModel mainAccountPieViewModel, AccountsAreaChartViewModel mainAccountAreaViewModel, InvestorLineChartsViewModel investorChartViewModel) {
            _investorsStore = investorsStore;
            _investorsStore.StateChanged += InvestorsStore_StateChanged;

            DepositCommand = depositAsyncCommand;
            AddInvestorCommand = addInvestorAsyncCommand;
            RemoveInvestorCommand = removeInvestorAsyncCommand;

            MainAccountPieViewModel = mainAccountPieViewModel;
            MainAccountAreaViewModel = mainAccountAreaViewModel;
            InvestorChartViewModel = investorChartViewModel;
        }

        private void InvestorsStore_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(Investors));
        }

    }
}
