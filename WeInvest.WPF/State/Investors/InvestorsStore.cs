using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Investors {
    public class InvestorsStore : IInvestorsStore {

        private ObservableCollection<Investor> _currentInvestors;

        public event EventHandler StateChanged;

        public ObservableCollection<Investor> CurrentInvestors {
            get => _currentInvestors; 
            set {
                _currentInvestors = value;
                OnStateChanged(EventArgs.Empty);
                CurrentInvestors.CollectionChanged += CurrentInvestors_CollectionChanged;
            }
        }

        private void CurrentInvestors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnStateChanged(e);
        }

        private void OnStateChanged(EventArgs e) {
            StateChanged?.Invoke(this, e);
        }

    }
}
