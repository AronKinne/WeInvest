using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WeInvest.Domain.Models;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.ViewModels.Controls;

namespace WeInvest.WPF.ViewModels {
    public class MainViewModel : WindowViewModelBase {

        private readonly IInvestorsStore _investorsStore;

        public ObservableCollection<Investor> Investors => _investorsStore.Investors;
        public ICommand MinimizeCommand { get; }
        public ICommand DepositCommand { get; }
        public ICommand AddInvestorCommand { get; }
        public ICommand RemoveInvestorCommand { get; }

        public DisplayedAccountPieChartViewModel MainAccountPieViewModel { get; }
        public AccountsAreaChartViewModel MainAccountAreaViewModel { get; }
        public InvestorLineChartsViewModel InvestorChartViewModel { get; }

        public MainViewModel(IInvestorsStore investorsStore, DepositAsyncCommand depositAsyncCommand, AddInvestorAsyncCommand addInvestorAsyncCommand, RemoveInvestorAsyncCommand removeInvestorAsyncCommand, DisplayedAccountPieChartViewModel mainAccountPieViewModel, AccountsAreaChartViewModel mainAccountAreaViewModel, InvestorLineChartsViewModel investorChartViewModel) : base() {
            _investorsStore = investorsStore;
            _investorsStore.StateChanged += InvestorsStore_StateChanged;

            MinimizeCommand = new RelayCommand(p => ((Window)p).WindowState = WindowState.Minimized);
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
