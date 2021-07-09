using System.Windows.Media;
using WeInvest.WPF.Utilities;

namespace WeInvest.WPF.ViewModels.Dialogs {
    public class InvestorDialogViewModel : DialogViewModelBase {

        public string InvestorName { get; set; }
        public Brush InvestorBrush { get; set; }
        public Brush[] BrushPool { get; set; }

        public InvestorDialogViewModel() {
            this.InvestorName = "";
            this.BrushPool = Utility.BrushesArray;

            this.OkayButtonContent = "Add";
        }

        protected override void Okay(object parameter) {
            InvestorName = InvestorName.Trim();

            base.Okay(parameter);
        }

        protected override bool CanOkay(object parameter) {
            return !string.IsNullOrWhiteSpace(InvestorName) && InvestorBrush != null;
        }

    }
}
