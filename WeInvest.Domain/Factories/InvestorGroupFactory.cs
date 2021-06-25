using WeInvest.Domain.Models;

namespace WeInvest.Domain.Factories {
    public class InvestorGroupFactory : IFactory<InvestorGroup> {

        private readonly IFactory<Investor> _investorFactory;
        private readonly IFactory<Account> _accountFactory;

        public InvestorGroupFactory(IFactory<Investor> investorFactory, IFactory<Account> accountFactory) {
            _investorFactory = investorFactory;
            _accountFactory = accountFactory;
        }

        public InvestorGroup Create() {
            return new InvestorGroup(_investorFactory, _accountFactory);
        }

        public InvestorGroup Create(object parameter) {
            return Create();
        }
    }
}
