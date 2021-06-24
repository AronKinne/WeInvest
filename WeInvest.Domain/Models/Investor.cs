using System.Collections.Generic;
using System.Windows.Media;
using WeInvest.Domain.Converters;

namespace WeInvest.Domain.Models {
    public class Investor {

        private readonly IListStringConverter _listConvertingService;
        private readonly IBrushStringConverter _brushConvertingService;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public string ShareHistoryString {
            get => _listConvertingService.ListToString<float>(ShareHistory);
            set => ShareHistory = _listConvertingService.StringToList<float>(value); 
        }

        public Brush Brush {
            get => _brushConvertingService.StringToBrush(ColorHex);
            set => ColorHex = _brushConvertingService.BrushToString(value);
        }
        public IList<float> ShareHistory { get; protected set; } = new List<float>() { 0 };
        public float Share { get => ShareHistory == null ? -1 : ShareHistory[ShareHistory.Count - 1]; }

        public Investor() {
            _listConvertingService = new ListStringConverter();
            _brushConvertingService = new BrushStringConverter();
        }

        public Investor(IListStringConverter listConvertingService, IBrushStringConverter brushConvertingService) {
            _listConvertingService = listConvertingService;
            _brushConvertingService = brushConvertingService;
        }

        public void Deposit(float amount) {
            if(ShareHistory.Count == 1 && Share == 0) {
                ShareHistory[0] = amount;
                return;
            }

            ShareHistory.Add(Share + amount);
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
