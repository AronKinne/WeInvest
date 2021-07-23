using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.Views.Controls.ViewModelInterfaces;

namespace WeInvest.WPF.ViewModels.Controls {
    public class StockPieChartViewModel : ViewModelBase, IPieChartViewModel {

        public IList<PieData> PieSeries { get; private set; } = new ObservableCollection<PieData>();

        public StockPieChartViewModel() {
            PieSeries.Add(new PieData(Brushes.Orange, 70));
            PieSeries.Add(new PieData(Brushes.LightGreen, 30));
            OnPropertyChanged(nameof(PieSeries));
        }

    }
}
