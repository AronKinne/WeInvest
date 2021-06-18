using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using WeInvest.Controls.Charts.Data;
using WeInvest.Models;

namespace WeInvest.ViewModels.Controls {
    public class MainAccountAreaControlViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public InvestorGroup InvestorGroup { get; set; }

        public ObservableCollection<OrderedAreaData> AreaDataSeries { get; set; }
        public ObservableCollection<Brush> ColorList { get; set; }

        public double AreaOpacity { get; set; }

        public MainAccountAreaControlViewModel(InvestorGroup investorGroup) {
            this.InvestorGroup = investorGroup;
            InvestorGroup.PropertyChanged += OnInvestorGroupPropertyChanged;

            this.AreaDataSeries = new ObservableCollection<OrderedAreaData>();
            this.ColorList = new ObservableCollection<Brush>() {
                Brushes.LightCoral,
                Brushes.LightSalmon,
                Brushes.Aquamarine
            };

            this.AreaOpacity = 1;
        }

        private void OnInvestorGroupPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(InvestorGroup.AccountHistory))
                UpdateAreaDataSeries();
            if(e.PropertyName == nameof(InvestorGroup.Investors))
                UpdateColorList();
        }

        private void UpdateAreaDataSeries() {
            AreaDataSeries = new ObservableCollection<OrderedAreaData>();

            foreach(var account in InvestorGroup.AccountHistory) {
                var doubleList = account.ToList().Select(p => (double)p.Value).ToList();
                AreaDataSeries.Add(new OrderedAreaData(AreaDataSeries.Count + 1, doubleList));
            }

            OnPropertyChanged(nameof(AreaDataSeries));
        }

        private void UpdateColorList() {
            ColorList = new ObservableCollection<Brush>();
            InvestorGroup.Investors.ForEach(i => {
                var color = i.Color.Clone();
                color.Opacity = AreaOpacity;
                ColorList.Add(color);
            });

            OnPropertyChanged(nameof(ColorList));
        }
    }
}
