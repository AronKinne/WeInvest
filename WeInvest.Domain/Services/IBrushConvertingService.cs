using System.Windows.Media;

namespace WeInvest.Domain.Services {
    public interface IBrushConvertingService {

        string BrushToString(Brush brush);
        Brush StringToBrush(string value);
    
    }
}
