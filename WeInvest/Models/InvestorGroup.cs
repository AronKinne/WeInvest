using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace WeInvest.Models {
    public class InvestorGroup : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public List<Investor> Investors { get; private set; }
        public List<Account> AccountHistory { get; private set; }
        public Account CurrentAccount { get => AccountHistory == null ? null : AccountHistory[AccountHistory.Count - 1]; }

        public InvestorGroup() {
            this.Investors = new List<Investor>();
            this.AccountHistory = new List<Account>();
        }

        public Investor AddInvestor(string name, Brush color) {
            Investor investor = new Investor(name, color);
            Investors.Add(investor);
            return investor;
        }

        public void Deposit(Investor investor, float amount) {
            if(!Investors.Contains(investor))
                throw new System.ArgumentException("This Investor is not part of this InvestorGroup. Add him first.", nameof(investor));

            investor.Deposit(amount);
            AccountHistory.Add(new Account(Investors));
            OnPropertyChanged(nameof(AccountHistory));
        }

    }
}
