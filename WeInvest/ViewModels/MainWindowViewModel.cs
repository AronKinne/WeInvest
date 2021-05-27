﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WeInvest.Models;
using WeInvest.ViewModels.Dialogs;
using WeInvest.ViewModels.Utilities;
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
        public ObservableCollection<KeyValuePair<Brush, float>> PieSeries { get; set; }

        #region Command Properties

        public RelayCommand DepositCommand { get; set; }

        #endregion

        public MainWindowViewModel() {
            this.InvestorGroup = new InvestorGroup();
            Investor stefan = InvestorGroup.AddInvestor("Stefan", Brushes.Coral);
            Investor aron = InvestorGroup.AddInvestor("Aron", Brushes.CornflowerBlue);

            this.DisplayedAccountIndex = 0;

            Deposit(stefan, 15);
            Deposit(aron, 85);
            Deposit(stefan, 50);

            #region Command Initializations

            this.DepositCommand = new RelayCommand(Deposit);

            #endregion
        }

        private void Deposit(Investor investor, float amount) {
            InvestorGroup.Deposit(investor, amount);
            DisplayedAccountIndex = InvestorGroup.AccountHistory.Count - 1;
        }

        public void UpdatePieSeries() {
            this.PieSeries = new ObservableCollection<KeyValuePair<Brush, float>>();
            DisplayedAccount?.ToList().ForEach(entry => {
                PieSeries.Add(new KeyValuePair<Brush, float>(entry.Key.Color, entry.Value));
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

        #endregion

    }
}
