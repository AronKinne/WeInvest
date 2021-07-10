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

        public ObservableCollection<Investor> Investors => _investorsStore.CurrentInvestors;
        public ICommand MinimizeCommand { get; }
        public ICommand DepositCommand { get; }
        public ICommand AddInvestorCommand { get; }
        public ICommand RemoveInvestorCommand { get; }

        public MainAccountPieControlViewModel MainAccountPieViewModel { get; }
        public MainAccountAreaControlViewModel MainAccountAreaViewModel { get; }
        public InvestorChartControlViewModel InvestorChartViewModel { get; }

        public MainViewModel(IInvestorsStore investorsStore, IAccountsStore accountsStore, DepositAsyncCommand depositAsyncCommand, AddInvestorAsyncCommand addInvestorAsyncCommand, RemoveInvestorAsyncCommand removeInvestorAsyncCommand) : base() {
            _investorsStore = investorsStore;
            _investorsStore.StateChanged += _investorsStore_StateChanged;

            MinimizeCommand = new RelayCommand(p => ((Window)p).WindowState = WindowState.Minimized);
            DepositCommand = depositAsyncCommand;
            AddInvestorCommand = addInvestorAsyncCommand;
            RemoveInvestorCommand = removeInvestorAsyncCommand;

            MainAccountPieViewModel = new MainAccountPieControlViewModel(accountsStore);
            MainAccountAreaViewModel = new MainAccountAreaControlViewModel(_investorsStore, accountsStore) { AreaOpacity = 1 };
            InvestorChartViewModel = new InvestorChartControlViewModel(_investorsStore);
        }

        private void _investorsStore_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(Investors));
        }
    }
}
