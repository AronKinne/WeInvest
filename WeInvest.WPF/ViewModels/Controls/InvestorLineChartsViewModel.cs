using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Controls {
    public class InvestorLineChartsViewModel : ViewModelBase {

        public IInvestorsStore InvestorsStore { get; }

        public IList<InvestorChartViewModel> DataSeries {
            get {
                if(InvestorsStore.Investors == null)
                    return null;

                var output = new ObservableCollection<InvestorChartViewModel>();

                foreach(var investor in InvestorsStore.Investors) {
                    var shareData = new ObservableCollection<OrderedLineData>();
                    for(int i = 0; i < investor.ShareHistory.Count; i++) {
                        shareData.Add(new OrderedLineData(i + 1, investor.ShareHistory[i]));
                    }

                    output.Add(new InvestorChartViewModel(investor, shareData));
                }

                return output;
            }
        }

        public InvestorLineChartsViewModel(IInvestorsStore investorsStore) {
            InvestorsStore = investorsStore;
            InvestorsStore.StateChanged += InvestorsStore_StateChanged;
        }

        private void InvestorsStore_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(DataSeries));
        }
    
    }
}
