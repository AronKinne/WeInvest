using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Investors {
    public class InvestorsStore : IInvestorsStore {

        private ObservableCollection<Investor> _investors;

        public event EventHandler StateChanged;

        public ObservableCollection<Investor> Investors {
            get => _investors; 
            set {
                _investors = value;
                OnStateChanged(EventArgs.Empty);
                Investors.CollectionChanged += Investors_CollectionChanged;
            }
        }

        private void Investors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnStateChanged(e);
        }

        private void OnStateChanged(EventArgs e) {
            StateChanged?.Invoke(this, e);
        }

    }
}
