using System.Windows;
using WeInvest.ViewModels.Dialogs;

namespace WeInvest.ViewModels.Utilities {
    class DialogService<TDialog, TViewModel> 
        where TDialog : Window, new()
        where TViewModel : DialogViewModelBase, new() {

        public TDialog Dialog { get; set; }
        public TViewModel ViewModel { get; set; }

        public DialogService() {
            this.Dialog = new TDialog();
            this.ViewModel = new TViewModel();

            Dialog.DataContext = ViewModel;
            ViewModel.RequestCloseDialog += (sender, e) => Dialog.DialogResult = true;
        }

        public bool? ShowDialog() {
            return Dialog?.ShowDialog();
        }

    }
}
