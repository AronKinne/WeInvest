using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;

namespace WeInvest.ViewModels.Controls {
    public class InvestorChartControlViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public InvestorGroup InvestorGroup { get; set; }

        public IList<InvestorData> DataSeries {
            get {
                if(InvestorGroup == null)
                    return null;

                var output = new ObservableCollection<InvestorData>();

                foreach(var investor in InvestorGroup.Investors) {
                    var shareData = new ObservableCollection<OrderedLineData>();

                    for(int i = 0; i < investor.ShareHistory.Count; i++) {
                        shareData.Add(new OrderedLineData(i + 1, investor.ShareHistory[i]));
                    }

                    output.Add(new InvestorData(investor, shareData));
                }

                return output;
            }
        }

        public InvestorChartControlViewModel(InvestorGroup investorGroup) {
            this.InvestorGroup = investorGroup;
            InvestorGroup.PropertyChanged += OnInvestorGroupPropertyChanged;
        }

        private void OnInvestorGroupPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(InvestorGroup.Investors))
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
