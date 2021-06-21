using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;

namespace WeInvest.ViewModels.Controls {
    public class MainAccountPieControlViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        private int _displayedAccountIndex = 0;
        private Account _displayedAccount;

        public InvestorGroup InvestorGroup { get; set; }

        public int MaxAccountIndex { get => InvestorGroup.AccountHistory.Count - 1; }
        public int DisplayedAccountIndex {
            get => _displayedAccountIndex;
            set {
                _displayedAccountIndex = Math.Max(0, Math.Min(value, InvestorGroup.AccountHistory.Count - 1));
                if(InvestorGroup.AccountHistory?.Count > 0)
                    DisplayedAccount = InvestorGroup.AccountHistory[DisplayedAccountIndex];
                OnPropertyChanged();
            }
        }
        public Account DisplayedAccount {
            get => _displayedAccount;
            set {
                _displayedAccount = value;
                UpdatePieSeries();
            }
        }
        public IList<PieData> PieSeries { get; set; }

        public MainAccountPieControlViewModel(InvestorGroup investorGroup) {
            this.InvestorGroup = investorGroup;
            InvestorGroup.PropertyChanged += OnInvestorGroupPropertyChanged;
        }

        private void OnInvestorGroupPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(InvestorGroup.AccountHistory)) {
                OnPropertyChanged(nameof(MaxAccountIndex));
            }
        }

        private void UpdatePieSeries() {
            this.PieSeries = new ObservableCollection<PieData>();
            var accountList = DisplayedAccount?.ToList();
            foreach(var entry in accountList)
                PieSeries.Add(new PieData(entry.Key.Brush, entry.Value));

            OnPropertyChanged(nameof(PieSeries));
        }

    }
}
