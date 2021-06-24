using System.Windows.Media;

namespace WeInvest.Domain.Converters {
    public interface IBrushStringConverter {

        string BrushToString(Brush brush);
        Brush StringToBrush(string value);
    
    }
}
