using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeInvest.Controls.Charts.Data;
using WeInvest.Utilities;

namespace WeInvest.Controls.Charts {
    public class LineChart : Canvas {

        public ObservableCollection<OrderedLineData> DataSeries {
            get { return (ObservableCollection<OrderedLineData>)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries",
                typeof(ObservableCollection<OrderedLineData>),
                typeof(LineChart),
                new PropertyMetadata(new ObservableCollection<OrderedLineData>(), OnDataSeriesChanged));

        private static void OnDataSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineChart)d).Update();
        }


        public Brush LineColor {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register("LineColor", typeof(Brush), typeof(LineChart), new PropertyMetadata(Brushes.Black, OnLineColorChanged));

        private static void OnLineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineChart)d).Update();
        }


        public int LineThickness {
            get { return (int)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThickness", typeof(int), typeof(LineChart), new PropertyMetadata(1, OnLineThicknessChanged));

        private static void OnLineThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineChart)d).Update();
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            Update();
        }

        public int Padding { get; set; }
        public int MinX { get; private set; }
        public int MaxX { get; private set; }
        public int MinY { get; private set; }
        public int MaxY { get; private set; }
        public double MinYValue { get; private set; }
        public double MaxYValue { get; private set; }

        public Path Line { get; private set; }

        public Line XAxis { get; private set; }
        public Line YAxis { get; private set; }

        public List<Label> XLabels { get; private set; }

        static LineChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        public LineChart() {
            this.Padding = 20;
            this.Line = new Path();
            this.XLabels = new List<Label>();

            Update();
        }

        public void Update() {
            UpdateMinMax();
            UpdateAxis();

            if(DataSeries.Count < 2)
                return;

            var orderedPoints = CreateOrderedPoints();
            UpdateLine(orderedPoints);
            UpdateLabels(orderedPoints);
        }

        private void UpdateMinMax() {
            this.MinX = 0 + Padding;
            this.MaxX = (int)ActualWidth - Padding;

            this.MinY = (int)ActualHeight - Padding;
            this.MaxY = 0 + Padding;

            this.MinYValue = double.PositiveInfinity;
            this.MaxYValue = double.NegativeInfinity;

            foreach(var data in DataSeries) {
                MinYValue = Math.Min(MinYValue, data.Value);
                MaxYValue = Math.Max(MaxYValue, data.Value);
            }
        }

        private void UpdateAxis() {
            Children.Remove(XAxis);
            Children.Remove(YAxis);

            XAxis = new Line() { X1 = MinX, Y1 = MinY, X2 = ActualWidth, Y2 = MinY, Stroke = Brushes.Black };
            YAxis = new Line() { X1 = MinX, Y1 = MinY, X2 = MinX, Y2 = 0, Stroke = Brushes.Black };

            Children.Add(XAxis);
            Children.Add(YAxis);
        }

        private List<Point> CreateOrderedPoints() {
            var output = new List<Point>();

            for(int i = 0; i < DataSeries.Count; i++) {
                var point = CalculatePoint(i);
                output.Add(point);
            }

            return output;
        }

        private Point CalculatePoint(int index) {
            if(index >= DataSeries.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index bigger than length of data series.");

            OrderedLineData data = DataSeries[index];

            int x = (int)Utility.Map(index, 0, DataSeries.Count - 1, MinX, MaxX);
            int y = (int)Utility.Map(data.Value, MinYValue, MaxYValue, MinY, MaxY);

            return new Point(x, y);
        }

        private void UpdateLine(List<Point> orderedPoints) {
            Children.Remove(Line);

            Line.Data = CreateLineData(orderedPoints);
            Line.Stroke = LineColor;
            Line.StrokeThickness = LineThickness;

            Children.Add(Line);
        }

        private Geometry CreateLineData(List<Point> orderedPoints) {
            var centerPoint = orderedPoints[0];

            var figure = new PathFigure { StartPoint = centerPoint };

            for(int i = 1; i < orderedPoints.Count; i++) {
                var segment = new LineSegment(orderedPoints[i], true);
                figure.Segments.Add(segment);
            }

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            return geometry;
        }

        private void UpdateLabels(List<Point> orderedPoints) {
            foreach(var label in XLabels)
                Children.Remove(label);

            XLabels = new List<Label>();

            for(int i = 0; i < DataSeries.Count; i++) {
                Label label = new Label() {
                    Content = DataSeries[i].Key.ToString(),
                    FontSize = Padding * .7
                };
                label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size size = label.DesiredSize;

                Children.Add(label);
                Canvas.SetLeft(label, orderedPoints[i].X - size.Width / 2);
                Canvas.SetTop(label, ActualHeight - Padding / 2 - size.Height / 2);

                XLabels.Add(label);
            }
        }
    }
}
