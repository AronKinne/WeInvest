using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeInvest.Controls.Charts.Data;

namespace WeInvest.Controls.Charts {
    public class LineChart : Canvas {

        public Path Line { get; set; }

        public ObservableCollection<LineData> DataSeries {
            get { return (ObservableCollection<LineData>)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries", typeof(ObservableCollection<LineData>), typeof(LineChart), new PropertyMetadata(null, OnDataSeriesChanged));

        private static void OnDataSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineChart)d).UpdateLine();
        }


        public Brush LineColor {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register("LineColor", typeof(Brush), typeof(LineChart), new PropertyMetadata(Brushes.Black, OnLineColorChanged));

        private static void OnLineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineChart)d).UpdateLine();
        }

        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }

        static LineChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        public LineChart() {
            Line = new Path();
            UpdateLine();
        }

        private void UpdateLine() {
            // TODO
        }
    }
}
