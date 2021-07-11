using System.Collections.ObjectModel;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Investors {
    public interface IInvestorsStore : IStore {

        ObservableCollection<Investor> Investors { get; set; }

    }
}
