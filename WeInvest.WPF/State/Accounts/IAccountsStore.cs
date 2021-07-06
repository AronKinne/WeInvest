using System.Collections.ObjectModel;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Accounts {
    public interface IAccountsStore : IStore {

        ObservableCollection<Account> CurrentAccounts { get; set; }

    }
}
