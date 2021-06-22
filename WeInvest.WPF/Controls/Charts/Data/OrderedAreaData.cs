using System.Collections.Generic;

namespace WeInvest.WPF.Controls.Charts.Data {
    public class OrderedAreaData : ChartData<object, IList<double>> {

        public OrderedAreaData(object key, IList<double> value) : base(key, value) { }

    }
}
