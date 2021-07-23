using System.Collections.Generic;
using WeInvest.WPF.Controls.Charts.Data;

namespace WeInvest.WPF.Views.Controls.ViewModelInterfaces {
    public interface IPieChartViewModel {

         IList<PieData> PieSeries { get; }

    }
}
