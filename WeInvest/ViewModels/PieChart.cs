using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WeInvest.ViewModels {
    public class PieChart : Canvas, INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public double Radius { get; set; }

        public Collection<KeyValuePair<Brush, float>> PieSeries {
            get { return (Collection<KeyValuePair<Brush, float>>)GetValue(PieSeriesProperty); }
            set {
                SetValue(PieSeriesProperty, value);
                OnPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for PieSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PieSeriesProperty =
            DependencyProperty.Register("PieSeries", typeof(Collection<KeyValuePair<Brush, float>>), typeof(PieChart), new PropertyMetadata(null, OnPieSeriesChanged));

        private static void OnPieSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PieChart)d).UpdateSectors();
        }

        static PieChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChart), new FrameworkPropertyMetadata(typeof(PieChart)));
        }

        public PieChart() {

        }

        private void UpdateSectors() {
            Children.Clear();

            float sum = 0;
            foreach(KeyValuePair<Brush, float> pieData in PieSeries) {
                sum += pieData.Value;
            }

            double offset = 0;
            foreach(KeyValuePair<Brush, float> pieData in PieSeries) {
                double angle = pieData.Value / sum * 2 * Math.PI;
                Path sector = new Path();
                sector.Data = CreateSectorData(offset, angle);
                sector.Fill = pieData.Key;
                Children.Add(sector);
                offset += angle;
            }
        }

        private Geometry CreateSectorData(double offset, double angle) {
            var centerPoint = new Point(CenterX, CenterY);

            var startPoint = new Point(
                CenterX + Radius * Math.Cos(offset - Math.PI / 2),
                CenterY + Radius * Math.Sin(offset - Math.PI / 2));

            var endPoint = new Point(
                CenterX + Radius * Math.Cos(offset - Math.PI / 2 + angle),
                CenterY + Radius * Math.Sin(offset - Math.PI / 2 + angle));

            var toStartLine = new LineSegment(startPoint, true);

            var arc = new ArcSegment(
                endPoint, 
                new Size(Radius, Radius),
                0,
                angle >= Math.PI,
                SweepDirection.Clockwise, true);

            var toCenterLine = new LineSegment(centerPoint, true);

            var figure = new PathFigure { 
                StartPoint = centerPoint,
                Segments = {
                    toStartLine,
                    arc,
                    toCenterLine
                }
            };

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            return geometry;
        }

        private double DegreesToRadians(double angle) {
            return (Math.PI / 180) * angle;
        }

    }
}
