using System.Windows.Media;

namespace WeInvest.Utilities.Services {
    public interface IBrushConvertingService {

        string BrushToString(Brush brush);
        Brush StringToBrush(string value);
    
    }
}
