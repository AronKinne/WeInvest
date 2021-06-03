using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;
using WeInvest.Utilities;
using WeInvest.ViewModels.Commands;
using WeInvest.ViewModels.Controls;

namespace WeInvest.ViewModels {
    public class MainWindowViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public InvestorGroup InvestorGroup { get; set; }
        public ObservableCollection<Investor> Investors { get => new ObservableCollection<Investor>(InvestorGroup?.Investors); }

        public MainAccountPieControlViewModel MainAccountPieViewModel { get; set; }
        public MainAccountAreaControlViewModel MainAccountAreaViewModel { get; set; }
        public InvestorChartControlViewModel InvestorChartViewModel { get; set; }

        #region Command Properties

        public RelayCommand DepositCommand { get; set; }

        public RelayCommand AddInvestorCommand { get; set; }

        #endregion

        public MainWindowViewModel() {

            this.InvestorGroup = new InvestorGroup();
            Investor stefan = AddInvestor("Stefan", Brushes.Coral);
            Investor aron = AddInvestor("Aron", Brushes.CornflowerBlue);

            this.MainAccountPieViewModel = new MainAccountPieControlViewModel(InvestorGroup);
            this.MainAccountAreaViewModel = new MainAccountAreaControlViewModel(InvestorGroup);
            this.InvestorChartViewModel = new InvestorChartControlViewModel(InvestorGroup);

            Deposit(stefan, 15);
            Deposit(aron, 85);
            Deposit(stefan, 50);

            #region Command Initializations

            this.DepositCommand = new RelayCommand(Deposit);

            this.AddInvestorCommand = new RelayCommand(AddInvestor);

            #endregion
        }

        private Investor AddInvestor(string name, Brush color) {
            var investor = InvestorGroup.AddInvestor(name, color);

            OnPropertyChanged(nameof(Investors));

            return investor;
        }

        private void Deposit(Investor investor, float amount) {
            InvestorGroup.Deposit(investor, amount);
            MainAccountPieViewModel.DisplayedAccountIndex = InvestorGroup.AccountHistory.Count - 1;

            OnPropertyChanged(nameof(Investors));
        }

        #region Commands

        private void Deposit(object parameter) {
            //var dialogService = new DialogService<DepositDialog, DepositDialogViewModel>();

            //if(dialogService.ShowDialog() == true) {
            //    Console.WriteLine();
            //}

            Random random = new Random();
            Deposit(Investors[random.Next(Investors.Count)], random.Next(5, 100));
        }

        private void AddInvestor(object parameter) {
            //var dialogService = new DialogService<InvestorDialog, InvestorDialogViewModel>();

            //if(dialogService.ShowDialog() == true) {
            //    Console.WriteLine();
            //}

            Random random = new Random();
            AddInvestor($"Tester {Investors.Count + 1}", Utility.BrushesArray[random.Next(Utility.BrushesArray.Length)]);
        }

        #endregion

    }
}
