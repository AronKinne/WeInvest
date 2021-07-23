using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using WeInvest.WPF.Controls.Charts.Data;

namespace WeInvest.WPF.Views.Controls.ViewModelInterfaces {
    public interface IAreaChartViewModel {

        IList<OrderedAreaData> AreaDataSeries { get; }
        IList<Brush> OrderedBrushList { get; }
        ICommand SelectionChangedCommand { get; }

    }
}
