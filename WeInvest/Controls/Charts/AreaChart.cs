using System.Windows;
using System.Windows.Controls;

namespace WeInvest.Controls.Charts {
    public class AreaChart : Control {
        static AreaChart() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AreaChart), new FrameworkPropertyMetadata(typeof(AreaChart)));
        }
    }
}
