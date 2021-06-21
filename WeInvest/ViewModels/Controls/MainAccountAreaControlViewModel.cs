using System.Collections.Generic;
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

        public IList<OrderedAreaData> AreaDataSeries { get; set; } = new ObservableCollection<OrderedAreaData>();
        public IList<Brush> BrushList { get; set; } = new ObservableCollection<Brush>();

        public double AreaOpacity { get; set; } = 1;

        public MainAccountAreaControlViewModel(InvestorGroup investorGroup) {
            this.InvestorGroup = investorGroup;
            InvestorGroup.PropertyChanged += OnInvestorGroupPropertyChanged;

            BrushList.Add(Brushes.LightCoral);
            BrushList.Add(Brushes.LightSalmon);
            BrushList.Add(Brushes.Aquamarine);
        }

        private void OnInvestorGroupPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(InvestorGroup.AccountHistory))
                UpdateAreaDataSeries();
            if(e.PropertyName == nameof(InvestorGroup.Investors))
                UpdateBrushList();
        }

        private void UpdateAreaDataSeries() {
            AreaDataSeries = new ObservableCollection<OrderedAreaData>();

            foreach(var account in InvestorGroup.AccountHistory) {
                var doubleList = account.ToList().Select(p => (double)p.Value).ToList();
                AreaDataSeries.Add(new OrderedAreaData(AreaDataSeries.Count + 1, doubleList));
            }

            OnPropertyChanged(nameof(AreaDataSeries));
        }

        private void UpdateBrushList() {
            BrushList = new ObservableCollection<Brush>();

            foreach(var investor in InvestorGroup.Investors) {
                var brush = investor.Brush.Clone();
                brush.Opacity = AreaOpacity;
                BrushList.Add(brush);
            }

            OnPropertyChanged(nameof(BrushList));
        }
    }
}
