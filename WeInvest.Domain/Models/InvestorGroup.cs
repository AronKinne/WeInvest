using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace WeInvest.Domain.Models {
    public class InvestorGroup : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public IList<Investor> Investors { get; private set; } = new List<Investor>();
        public IList<Account> AccountHistory { get; private set; } = new List<Account>();
        public Account CurrentAccount { get => AccountHistory == null ? null : AccountHistory[AccountHistory.Count - 1]; }

        public Investor AddInvestor(string name, Brush brush) {
            Investor investor = new Investor() {
                Name = name,
                Brush = brush
            };
            Investors.Add(investor);
            AddInvestorToAccountHistory(investor);

            OnPropertyChanged(nameof(Investors));

            return investor;
        }

        public void Deposit(Investor investor, float amount) {
            if(!Investors.Contains(investor))
                throw new System.ArgumentException("This Investor is not part of this InvestorGroup. Add him first.", nameof(investor));

            investor.Deposit(amount);
            Account account = new Account();
            account.AddOwners(Investors);
            AccountHistory.Add(account);

            OnPropertyChanged(nameof(AccountHistory));
            OnPropertyChanged(nameof(Investors));
        }

        private void AddInvestorToAccountHistory(Investor investor) {
            foreach(var account in AccountHistory)
                account.AddOwner(investor, 0);

            OnPropertyChanged(nameof(AccountHistory));
        }

    }
}
