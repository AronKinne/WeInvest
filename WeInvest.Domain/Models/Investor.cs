using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Domain.Converters;

namespace WeInvest.Domain.Models {
    public class Investor {

        private readonly IListStringConverter _listStringConverter;
        private readonly IBrushStringConverter _brushStringConverter;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public string ShareHistoryString {
            get => _listStringConverter.ListToString(ShareHistory);
            set => ShareHistory = _listStringConverter.StringToList<float>(value); 
        }

        public Brush Brush {
            get => _brushStringConverter.StringToBrush(ColorHex);
            set => ColorHex = _brushStringConverter.BrushToString(value);
        }
        public IList<float> ShareHistory { get; set; } = new List<float>() { 0 };
        public float Share { get => ShareHistory == null ? 0 : ShareHistory[ShareHistory.Count - 1]; }

        public Investor(IListStringConverter listStringConverter, IBrushStringConverter brushStringConverter) {
            _listStringConverter = listStringConverter;
            _brushStringConverter = brushStringConverter;
        }

        public override string ToString() {
            string output = Name + " (";

            for(int i = 0; i < ShareHistory.Count; i++) {
                var share = ShareHistory[i];
                output += share + (i == ShareHistory.Count - 1 ? ")" : ", ");
            }

            return output;
        }


    }
}
