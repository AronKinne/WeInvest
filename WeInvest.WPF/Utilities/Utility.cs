using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WeInvest.WPF.Utilities {
    public static class Utility {

        public static double Map(double value, double low1, double high1, double low2, double high2) {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

        public static Brush[] BrushesArray {
            get {
                return typeof(Brushes).GetProperties().Select(p => p.GetValue(null) as Brush).ToArray();
            } 
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static async void FireAndForget(this Task task) {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            try {
                await task;
            } catch(Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
