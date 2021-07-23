using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.Views.Controls.ViewModelInterfaces;

namespace WeInvest.WPF.ViewModels.Controls {
    public class StockAreaChartViewModel : ViewModelBase, IAreaChartViewModel {

        public IList<OrderedAreaData> AreaDataSeries { get; private set; } = new ObservableCollection<OrderedAreaData>();
        public IList<Brush> OrderedBrushList { get; private set; } = new ObservableCollection<Brush>();

        public ICommand SelectionChangedCommand { get; }

        public StockAreaChartViewModel() {
            AreaDataSeries.Add(new OrderedAreaData(0, new List<double>() { 60, 40 }));
            AreaDataSeries.Add(new OrderedAreaData(0, new List<double>() { 50, 50 }));
            AreaDataSeries.Add(new OrderedAreaData(1, new List<double>() { 30, 70 }));
            OnPropertyChanged(nameof(AreaDataSeries));

            OrderedBrushList.Add(Brushes.LightGreen);
            OrderedBrushList.Add(Brushes.Orange);
            OnPropertyChanged(nameof(OrderedBrushList));
        }
    }
}
