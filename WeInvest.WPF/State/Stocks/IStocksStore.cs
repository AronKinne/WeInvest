using System.Collections.ObjectModel;
using WeInvest.Domain.Models;

namespace WeInvest.WPF.State.Stocks {
    public interface IStocksStore : IStore {

        ObservableCollection<Stock> Stocks { get; set; }

    }
}
