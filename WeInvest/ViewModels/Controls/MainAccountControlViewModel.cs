using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;

namespace WeInvest.ViewModels.Controls {
    public class MainAccountControlViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        private int _displayedAccountIndex;
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
        public ObservableCollection<PieData> PieSeries { get; set; }

        public MainAccountControlViewModel(InvestorGroup investorGroup) {
            this.InvestorGroup = investorGroup;
            InvestorGroup.PropertyChanged += OnInvestorGroupPropertyChanged;

            this.DisplayedAccountIndex = 0;
        }

        private void OnInvestorGroupPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(InvestorGroup.AccountHistory)) {
                OnPropertyChanged(nameof(MaxAccountIndex));
            }
        }

        private void UpdatePieSeries() {
            this.PieSeries = new ObservableCollection<PieData>();
            DisplayedAccount?.ToList().ForEach(entry => {
                PieSeries.Add(new PieData(entry.Key.Color, entry.Value));
            });
            OnPropertyChanged(nameof(PieSeries));
        }

    }
}
