using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;
using WeInvest.Utilities.Services;
using WeInvest.ViewModels.Commands;
using WeInvest.ViewModels.Dialogs;
using WeInvest.Views.Dialogs;

namespace WeInvest.ViewModels {
    class MainWindowViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        private int _displayedAccountIndex;
        private Account _displayedAccount;

        public InvestorGroup InvestorGroup { get; set; }
        public ObservableCollection<Investor> Investors { get => new ObservableCollection<Investor>(InvestorGroup?.Investors); }

        public int DisplayedAccountIndex {
            get => _displayedAccountIndex;
            set {
                _displayedAccountIndex = Math.Max(0, Math.Min(value, InvestorGroup.AccountHistory.Count - 1));
                if(InvestorGroup.AccountHistory?.Count > 0)
                    DisplayedAccount = InvestorGroup.AccountHistory[DisplayedAccountIndex];
            }
        }
        public Account DisplayedAccount {
            get => _displayedAccount;
            set {
                _displayedAccount = value;
                UpdatePieSeries();
            }
        }
        public int MaxAccountIndex { get => InvestorGroup.AccountHistory.Count - 1; }

        public ObservableCollection<PieData> PieSeries { get; set; }
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

            this.DisplayedAccountIndex = 0;

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
            DisplayedAccountIndex = InvestorGroup.AccountHistory.Count - 1;
        }

        public void UpdatePieSeries() {
            this.PieSeries = new ObservableCollection<PieData>();
            DisplayedAccount?.ToList().ForEach(entry => {
                PieSeries.Add(new PieData(entry.Key.Color, entry.Value));
            });
            OnPropertyChanged(nameof(PieSeries));
        }

        #region Commands

        private void Deposit(object parameter) {
            var dialogService = new DialogService<DepositDialog, DepositDialogViewModel>();

            if(dialogService.ShowDialog() == true) {
                System.Console.WriteLine();
            }
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
