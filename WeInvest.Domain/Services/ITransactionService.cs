using WeInvest.Domain.Models;

namespace WeInvest.Domain.Services {
    public interface ITransactionService {

        void Deposit(Investor investor, float amount);

    }
}
