using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Accounts {
    public interface IDisplayedAccountStore : IStore {

        Account DisplayedAccount { get; set; }

    }
}
