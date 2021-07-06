using WeInvest.Domain.Models;

namespace WeInvest.Domain.Services {
    public class TransactionService : ITransactionService {

        public void Deposit(Investor investor, float amount) {
            if(investor.ShareHistory.Count == 1 && investor.Share == 0) {
                investor.ShareHistory[0] = amount;
                return;
            }

            investor.ShareHistory.Add(investor.Share + amount);
        }

    }
}
