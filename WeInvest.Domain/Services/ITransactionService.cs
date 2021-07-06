using System.Threading.Tasks;
using WeInvest.Domain.Models;

namespace WeInvest.Domain.Services {
    public interface ITransactionService {

        Task<Investor> DepositAsync(Investor investor, float amount);

    }
}
