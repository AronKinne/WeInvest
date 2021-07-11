using System.Collections.Generic;
using System.Collections.ObjectModel;
using WeInvest.Domain.Models;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.State.Investors;

namespace WeInvest.WPF.ViewModels.Controls {
    public class InvestorChartControlViewModel : ViewModelBase {

        public IInvestorsStore InvestorsStore { get; }

        public IList<InvestorData> DataSeries {
            get {
                if(InvestorsStore.Investors == null)
                    return null;

                var output = new ObservableCollection<InvestorData>();

                foreach(var investor in InvestorsStore.Investors) {
                    var shareData = new ObservableCollection<OrderedLineData>();

                    for(int i = 0; i < investor.ShareHistory.Count; i++) {
                        shareData.Add(new OrderedLineData(i + 1, investor.ShareHistory[i]));
                    }

                    output.Add(new InvestorData(investor, shareData));
                }

                return output;
            }
        }

        public InvestorChartControlViewModel(IInvestorsStore investorsStore) {
            InvestorsStore = investorsStore;
            InvestorsStore.StateChanged += InvestorsStore_StateChanged;
        }

        private void InvestorsStore_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(DataSeries));
        }

        public class InvestorData {

            public Investor Investor { get; set; }
            public IList<OrderedLineData> ShareData { get; set; }

            public InvestorData(Investor investor, IList<OrderedLineData> shareData) {
                this.Investor = investor;
                this.ShareData = shareData;
            }

        }
    
    }
}
