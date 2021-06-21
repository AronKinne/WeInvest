using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeInvest.Controls.Charts.Data;
using WeInvest.Utilities;

namespace WeInvest.Controls.Charts {
    public class AreaChart : XYChart<OrderedAreaData> {

        public IList<Brush> OrderedBrushList {
            get { return (IList<Brush>)GetValue(OrderedBrushListProperty); }
            set { SetValue(OrderedBrushListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderedBrushList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderedBrushListProperty =
            DependencyProperty.Register("OrderedBrushList",
                typeof(IList<Brush>),
                typeof(AreaChart),
                new PropertyMetadata(new ObservableCollection<Brush>(), OnOrderedBrushListChanged));

        private static void OnOrderedBrushListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((AreaChart)d).Update();
        }


        public IList<Path> Areas { get; private set; } = new List<Path>();
        public IList<Label> XLabels { get; private set; } = new List<Label>();

        static AreaChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AreaChart), new FrameworkPropertyMetadata(typeof(AreaChart)));
        }

        public AreaChart() {
            this.Padding = 20;
        }

        public override void Update() {
            UpdateMinMax();
            UpdateAxis();

            if(DataSeries.Count < 2)
                return;

            var points = CreateBoundaryPoints();
            UpdateArea(points);
            UpdateLabels(points);
        }

        private Point[,] CreateBoundaryPoints() {
            int amtLayers = DataSeries[0].Value.Count;
            int amtValues = DataSeries.Count;

            var output = new Point[amtLayers, amtValues];
            
            for(int valueIndex = 0; valueIndex < DataSeries.Count; valueIndex++) {
                double realX = Utility.Map(valueIndex, 0, DataSeries.Count - 1, MinX, MaxX);

                var yValues = CreateBoundaryYValues(valueIndex);
                if(yValues.Length != amtLayers)
                    throw new FormatException("AreaChart -> DataSeries -> OrderedAreaData -> Value (IList<double>): Every value within the DataSeries shall have the same length.");

                for(int layerIndex = 0; layerIndex < yValues.Length; layerIndex++) {
                    double realY = yValues[layerIndex];
                    output[layerIndex, valueIndex] = new Point(realX, realY);
                }
            }

            return output;  // returns in this case: https://www.desmos.com/calculator/jda146ulmk
        }

        private double[] CreateBoundaryYValues(int index) {
            if(index >= DataSeries.Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index has to be in range of " + nameof(DataSeries) + " array.");

            int amtLayers = DataSeries[index].Value.Count;
            var yValues = new double[amtLayers];

            IList<double> values = DataSeries[index].Value;

            double sum = 0;
            foreach(var value in values)
                sum += value;

            double lastProp = 0;
            for(int i = 0; i < values.Count; i++) {
                double proportionalY = values[i] / sum + lastProp;
                double realY = Utility.Map(proportionalY, 0, 1, MaxY, MinY);
                yValues[i] = realY;

                lastProp = proportionalY;
            }

            return yValues;
        }

        private void UpdateArea(Point[,] points) {
            Random random = new Random();

            foreach(var path in Areas)
                Children.Remove(path);
            Areas = new List<Path>();

            for(int layer = 0; layer < points.GetLength(0); layer++) {
                var area = new Path();
                area.Data = CreateArea(points, layer);

                var brush = Utility.BrushesArray[random.Next(Utility.BrushesArray.Length)];
                if(OrderedBrushList?.Count - 1 >= layer)
                    brush = OrderedBrushList[layer];
                area.Fill = brush;

                Areas.Add(area);
                Children.Add(area);
            }
        }

        private Geometry CreateArea(Point[,] points, int layer) {
            if(layer < 0 || layer >= points.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(layer), "Layer index has to be in range of the first dimension of the " + nameof(points) + " array.");

            int lastLayer = layer - 1;

            var cornerPoints = new List<Point>();
            if(lastLayer < 0) {
                cornerPoints.Add(new Point(MaxX, MaxY));
                cornerPoints.Add(new Point(MinX, MaxY));
            } else {
                var lastLayerPoints = Enumerable.Range(0, points.GetLength(1)).Select(i => points[lastLayer, i]).ToArray().Reverse();
                cornerPoints.AddRange(lastLayerPoints);
            }

            var layerPoints = Enumerable.Range(0, points.GetLength(1)).Select(i => points[layer, i]).ToArray();
            cornerPoints.AddRange(layerPoints);

            var figure = new PathFigure() { StartPoint = cornerPoints[0] };
            cornerPoints.Add(cornerPoints[0]);
            cornerPoints = cornerPoints.ToArray().Where((p, i) => i != 0).ToList();

            var segment = new PolyLineSegment(cornerPoints, true);
            figure.Segments.Add(segment);

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            return geometry;
        }

        private void UpdateLabels(Point[,] points) {
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
                Canvas.SetLeft(label, points[0, i].X - size.Width / 2);
                Canvas.SetTop(label, ActualHeight - Padding / 2 - size.Height / 2);

                XLabels.Add(label);
            }
        }
    }
}
