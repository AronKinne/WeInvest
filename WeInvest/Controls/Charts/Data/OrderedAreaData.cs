using System.Collections.Generic;

namespace WeInvest.Controls.Charts.Data {
    public class OrderedAreaData : ChartData<object, List<double>> {

        public OrderedAreaData(object key, List<double> value) : base(key, value) { }

    }
}
