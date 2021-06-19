using WeInvest.Models;

namespace WeInvest.Utilities.Factories {
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
