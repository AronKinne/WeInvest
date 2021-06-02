using System.Linq;
using System.Windows.Media;

namespace WeInvest.Utilities {
    public static class Utility {

        public static double Map(double value, double low1, double high1, double low2, double high2) {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

        public static Brush[] BrushesArray {
            get {
                return typeof(Brushes).GetProperties().Select(p => p.GetValue(null) as Brush).ToArray();
            } 
        }

    }
}
