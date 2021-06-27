using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.Utilities.Services;
using WeInvest.WPF.ViewModels.Commands;
using WeInvest.WPF.ViewModels.Controls;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.ViewModels {
    public class MainWindowViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public InvestorGroup InvestorGroup { get; set; }
        public IList<Investor> Investors { get => new ObservableCollection<Investor>(InvestorGroup?.Investors); }

        public MainAccountPieControlViewModel MainAccountPieViewModel { get; set; }
        public MainAccountAreaControlViewModel MainAccountAreaViewModel { get; set; }
        public InvestorChartControlViewModel InvestorChartViewModel { get; set; }

        public IFactory<Investor> InvestorFactory { get; set; }

        public IDataAccess<Investor> InvestorDataService { get; set; }

        #region Command Properties

        public ICommand DepositCommand { get; set; }
        public ICommand AddInvestorCommand { get; set; }

        #endregion

        public MainWindowViewModel() {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName);
            var serviceProvider = ServiceProviderFactory.Create();
            var investorGroupFactory = serviceProvider.GetRequiredService<IFactory<InvestorGroup>>();

            this.InvestorGroup = investorGroupFactory.Create();

            this.MainAccountPieViewModel = new MainAccountPieControlViewModel(InvestorGroup);
            this.MainAccountAreaViewModel = new MainAccountAreaControlViewModel(InvestorGroup) { AreaOpacity = 1 };
            this.InvestorChartViewModel = new InvestorChartControlViewModel(InvestorGroup);

            this.InvestorFactory = serviceProvider.GetRequiredService<IFactory<Investor>>();

            this.InvestorDataService = serviceProvider.GetRequiredService<IDataAccess<Investor>>();

            foreach(var investor in InvestorDataService.GetAllAsync().Result) {
                InvestorGroup.AddInvestor(investor);
            }

            //Deposit(stefan, 15);
            //Deposit(aron, 85);
            //Deposit(stefan, 50);

            #region Command Initializations

            this.DepositCommand = new RelayCommand(DepositFromDialog);
            this.AddInvestorCommand = new AsyncRelayCommand(AddInvestorFromDialogAsync);

            #endregion
        }

        private async Task<Investor> AddInvestorAsync(string name, Brush brush) {
            var investor = InvestorFactory.Create();
            investor.Name = name;
            investor.Brush = brush;

            var createdInvestor = await InvestorDataService.CreateAsync(investor);
            InvestorGroup.AddInvestor(createdInvestor);

            OnPropertyChanged(nameof(Investors));

            return createdInvestor;
        }

        private void Deposit(Investor investor, float amount) {
            InvestorGroup.Deposit(investor, amount);
            MainAccountPieViewModel.DisplayedAccountIndex = InvestorGroup.AccountHistory.Count - 1;

            OnPropertyChanged(nameof(Investors));
        }

        #region Commands

        private void DepositFromDialog(object parameter) {
            var dialogService = new DialogService<DepositDialog, DepositDialogViewModel>();

            if(dialogService.ShowDialog() == true) {

            }

            Random random = new Random();
            Deposit(Investors[random.Next(Investors.Count)], random.Next(20, 50));
        }

        private async Task AddInvestorFromDialogAsync(object parameter) {
            var dialogService = new DialogService<InvestorDialog, InvestorDialogViewModel>();

            if(dialogService.ShowDialog() == true) {
                var viewModel = dialogService.ViewModel;
                await AddInvestorAsync(viewModel.InvestorName, viewModel.InvestorBrush);
            }
        }

        #endregion

    }
}
