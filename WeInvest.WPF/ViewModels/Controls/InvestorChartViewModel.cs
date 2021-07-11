using System.Collections.Generic;
using WeInvest.Domain.Models;
using WeInvest.WPF.Controls.Charts.Data;

namespace WeInvest.WPF.ViewModels.Controls {
    public class InvestorChartViewModel : ViewModelBase {

        public Investor Investor { get; set; }
        public IList<OrderedLineData> ShareData { get; set; }

        public InvestorChartViewModel(Investor investor, IList<OrderedLineData> shareData) {
            this.Investor = investor;
            this.ShareData = shareData;
        }

    }
}
