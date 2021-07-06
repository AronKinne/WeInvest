using System.Threading.Tasks;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Services {
    public class TransactionService : ITransactionService {

        private readonly IDataAccess<Investor> _investorDataAccess;

        public TransactionService(IDataAccess<Investor> investorDataAccess) {
            _investorDataAccess = investorDataAccess;
        }

        public async Task<Investor> DepositAsync(Investor investor, float amount) {
            investor.ShareHistory.Add(investor.Share + amount);

            if(investor.ShareHistory[0] == 0)
                investor.ShareHistory.RemoveAt(0);

            return await _investorDataAccess.UpdateAsync(investor.Id, investor);
        }

    }
}
