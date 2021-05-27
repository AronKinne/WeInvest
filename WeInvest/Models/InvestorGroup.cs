using System.Collections.Generic;
using System.Windows.Media;

namespace WeInvest.Models {
    class InvestorGroup {

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
            investor.Deposit(amount);
            AccountHistory.Add(new Account(Investors));
        }

    }
}
