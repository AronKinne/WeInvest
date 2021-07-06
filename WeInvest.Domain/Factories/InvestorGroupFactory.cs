using WeInvest.Domain.Models;
using WeInvest.Domain.Services;

namespace WeInvest.Domain.Factories {
    public class InvestorGroupFactory : IFactory<InvestorGroup> {

        private readonly IFactory<Investor> _investorFactory;
        private readonly IFactory<Account> _accountFactory;
        private readonly ITransactionService _transactionService;

        public InvestorGroupFactory(IFactory<Investor> investorFactory, IFactory<Account> accountFactory, ITransactionService transactionService) {
            _investorFactory = investorFactory;
            _accountFactory = accountFactory;
            _transactionService = transactionService;
        }

        public InvestorGroup Create() {
            return new InvestorGroup(_investorFactory, _accountFactory, _transactionService);
        }

        public InvestorGroup Create(object parameter) {
            return Create();
        }
    }
}
