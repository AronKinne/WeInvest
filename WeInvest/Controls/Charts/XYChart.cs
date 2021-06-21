using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeInvest.Controls.Charts.Data;

namespace WeInvest.Controls.Charts {
    public abstract class XYChart<TData> : Canvas where TData : IChartData {

        public IList<TData> DataSeries {
            get { return (IList<TData>)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries",
                typeof(IList<TData>), 
                typeof(XYChart<TData>), 
                new PropertyMetadata(new ObservableCollection<TData>(), OnDataSeriesChanged));

        private static void OnDataSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((XYChart<TData>)d).Update();
        }


        public Brush AxisBrush {
            get { return (Brush)GetValue(AxisBrushProperty); }
            set { SetValue(AxisBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisBrushProperty =
            DependencyProperty.Register("AxisBrush", typeof(Brush), typeof(XYChart<TData>), new PropertyMetadata(Brushes.Black, OnAxisBrushChanged));

        private static void OnAxisBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((XYChart<TData>)d).Update();
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            Update();
        }


        public int Padding { get; set; }
        public int MinX { get; protected set; }
        public int MaxX { get; protected set; }
        public int MinY { get; protected set; }
        public int MaxY { get; protected set; }


        public Line XAxis { get; protected set; }
        public Line YAxis { get; protected set; }


        static XYChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XYChart<TData>), new FrameworkPropertyMetadata(typeof(XYChart<TData>)));
        }

        public abstract void Update();

        protected virtual void UpdateMinMax() {
            this.MinX = 0 + Padding;
            this.MaxX = (int)ActualWidth - Padding;

            this.MinY = (int)ActualHeight - Padding;
            this.MaxY = 0 + Padding;
        }

        protected virtual void UpdateAxis() {
            Children.Remove(XAxis);
            Children.Remove(YAxis);

            XAxis = new Line() { X1 = MinX, Y1 = MinY, X2 = ActualWidth, Y2 = MinY, Stroke = AxisBrush };
            YAxis = new Line() { X1 = MinX, Y1 = MinY, X2 = MinX, Y2 = 0, Stroke = AxisBrush };

            Children.Add(XAxis);
            Children.Add(YAxis);

            Canvas.SetZIndex(XAxis, 1);
            Canvas.SetZIndex(YAxis, 1);
        }
    }
}
