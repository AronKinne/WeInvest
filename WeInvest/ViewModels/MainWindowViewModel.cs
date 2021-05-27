using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace WeInvest.ViewModels {
    class MainWindowViewModel {

        public ObservableCollection<KeyValuePair<Brush, float>> PieSeries { get; set; }

        public MainWindowViewModel() {
            this.PieSeries = new ObservableCollection<KeyValuePair<Brush, float>>();
            PieSeries.Add(new KeyValuePair<Brush, float>(Brushes.Coral, 15));
            PieSeries.Add(new KeyValuePair<Brush, float>(Brushes.CornflowerBlue, 85));
        }

    }
}
