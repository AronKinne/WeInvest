using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Models {
    public class InvestorGroup : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        private readonly IFactory<Investor> _investorFactory;
        private readonly IFactory<Account> _accountFactory;
        private readonly ITransactionService _transactionService;

        public IList<Investor> Investors { get; private set; } = new List<Investor>();
        public IList<Account> AccountHistory { get; private set; } = new List<Account>();
        public Account CurrentAccount { get => AccountHistory == null ? null : AccountHistory[AccountHistory.Count - 1]; }

        public InvestorGroup(IFactory<Investor> investorFactory, IFactory<Account> accountFactory, ITransactionService transactionService) {
            _investorFactory = investorFactory;
            _accountFactory = accountFactory;
            _transactionService = transactionService;
        }

        public Investor AddInvestor(string name, Brush brush) {
            Investor investor = _investorFactory.Create();
            investor.Name = name;
            investor.Brush = brush;

            AddInvestor(investor);

            return investor;
        }

        public void AddInvestor(Investor investor) {
            Investors.Add(investor);
            AddInvestorToAccountHistory(investor);

            OnPropertyChanged(nameof(Investors));
        }

        public void Deposit(Investor investor, float amount) {
            if(!Investors.Contains(investor))
                throw new System.ArgumentException("This Investor is not part of this InvestorGroup. Add him first.", nameof(investor));

            _transactionService.Deposit(investor, amount);
            Account account = _accountFactory.Create();
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
