﻿using System.Windows.Media;
using WeInvest.Utilities;

namespace WeInvest.ViewModels.Dialogs {
    public class InvestorDialogViewModel : DialogViewModelBase {

        public string Title { get; set; }

        public string InvestorName { get; set; }
        public Brush InvestorBrush { get; set; }
        public Brush[] BrushPool { get; set; }

        public InvestorDialogViewModel() {
            this.Title = "Add Investor";

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
