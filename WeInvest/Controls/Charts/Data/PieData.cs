﻿using System.Windows.Media;

namespace WeInvest.Controls.Charts.Data {
    public class PieData : ChartData<Brush, double> {
        public PieData(Brush key, double value) : base(key, value) { }
    }
}
