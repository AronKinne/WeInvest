using WeInvest.Domain.Models;

namespace WeInvest.Domain.Factories {
    public class InvestorGroupFactory : IFactory<InvestorGroup> {

        private readonly IFactory<Investor> _investorFactory;

        public InvestorGroupFactory(IFactory<Investor> investorFactory) {
            _investorFactory = investorFactory;
        }

        public InvestorGroup Create() {
            return new InvestorGroup(_investorFactory);
        }

    }
}
