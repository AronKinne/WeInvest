using System.Windows.Media;

namespace WeInvest.Utilities.Services {
    public class BrushConvertingService : IBrushConvertingService {

        private readonly BrushConverter _brushConverter;

        public BrushConvertingService() {
            _brushConverter = new BrushConverter();
        }

        public string BrushToString(Brush brush) {
            return _brushConverter.ConvertToString(brush);
        }

        public Brush StringToBrush(string value) {
            return (Brush)_brushConverter.ConvertFromString(value);
        }
    }
}
