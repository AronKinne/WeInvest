using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;
using WeInvest.Utilities.Services;
using WeInvest.ViewModels.Commands;
using WeInvest.ViewModels.Controls;
using WeInvest.ViewModels.Dialogs;
using WeInvest.Views.Dialogs;

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

        public MainAccountControlViewModel MainAccountViewModel { get; set; }

        public ObservableCollection<OrderedLineData> InvestorLineData { get; set; }

        #region Command Properties

        public RelayCommand DepositCommand { get; set; }

        public RelayCommand AddInvestorCommand { get; set; }

        #endregion

        public MainWindowViewModel() {

            this.InvestorLineData = new ObservableCollection<OrderedLineData>() {
                new OrderedLineData(1, 10),
                new OrderedLineData(2, 15),
                new OrderedLineData(3, 25),
                new OrderedLineData(4, 20),
                new OrderedLineData(5, 5)
            };

            this.InvestorGroup = new InvestorGroup();
            Investor stefan = AddInvestor("Stefan", Brushes.Coral);
            Investor aron = AddInvestor("Aron", Brushes.CornflowerBlue);

            this.MainAccountViewModel = new MainAccountControlViewModel(InvestorGroup);

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
            MainAccountViewModel.DisplayedAccountIndex = InvestorGroup.AccountHistory.Count - 1;
        }

        #region Commands

        private void Deposit(object parameter) {
            var dialogService = new DialogService<DepositDialog, DepositDialogViewModel>();

            if(dialogService.ShowDialog() == true) {
                System.Console.WriteLine();
            }

            Deposit(Investors[0], 15);
        }

        private void AddInvestor(object parameter) {
            var dialogService = new DialogService<InvestorDialog, InvestorDialogViewModel>();

            if(dialogService.ShowDialog() == true) {
                Console.WriteLine();
            }

            AddInvestor($"Tester {Investors.Count + 1}", Brushes.Tomato);
        }

        #endregion

    }
}
