using System.Windows.Media;

namespace WeInvest.Domain.Converters {
    public class BrushStringConverter : IBrushStringConverter {

        private readonly BrushConverter _brushConverter;

        public BrushStringConverter() {
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
