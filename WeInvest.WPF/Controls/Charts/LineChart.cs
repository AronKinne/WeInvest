using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeInvest.WPF.Controls.Charts.Data;
using WeInvest.WPF.Utilities;

namespace WeInvest.WPF.Controls.Charts {
    public class LineChart : XYChart<OrderedLineData> {

        public Brush LineBrush {
            get { return (Brush)GetValue(LineBrushProperty); }
            set { SetValue(LineBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register("LineBrush", typeof(Brush), typeof(LineChart), new PropertyMetadata(Brushes.Black, OnLineBrushChanged));

        private static void OnLineBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
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


        public double MinYValue { get; private set; }
        public double MaxYValue { get; private set; }

        public Path Line { get; private set; } = new Path();
        public IList<Label> XLabels { get; private set; } = new List<Label>();

        static LineChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        public LineChart() {
            this.Padding = 20;

            Update();
        }

        public override void Update() {
            UpdateMinMax();
            UpdateAxis();

            if(DataSeries.Count < 2)
                return;

            var orderedPoints = CreateOrderedPoints();
            UpdateLine(orderedPoints);
            UpdateLabels(orderedPoints);
        }

        protected override void UpdateMinMax() {
            base.UpdateMinMax();

            this.MinYValue = double.PositiveInfinity;
            this.MaxYValue = double.NegativeInfinity;

            foreach(var data in DataSeries) {
                MinYValue = Math.Min(MinYValue, data.Value);
                MaxYValue = Math.Max(MaxYValue, data.Value);
            }
        }

        private IList<Point> CreateOrderedPoints() {
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

        private void UpdateLine(IList<Point> orderedPoints) {
            Children.Remove(Line);

            Line.Data = CreateLineData(orderedPoints);
            Line.Stroke = LineBrush;
            Line.StrokeThickness = LineThickness;

            Children.Add(Line);
        }

        private Geometry CreateLineData(IList<Point> orderedPoints) {
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

        private void UpdateLabels(IList<Point> orderedPoints) {
            foreach(var label in XLabels)
                Children.Remove(label);

            XLabels = new List<Label>();

            for(int i = 0; i < DataSeries.Count; i++) {
                Label label = new Label() {
                    Content = DataSeries[i].Key.ToString(),
                    FontSize = Padding * .7,
                    Foreground = AxisBrush
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
