using System.Windows;
using WeInvest.WPF.ViewModels.Dialogs;

namespace WeInvest.WPF.Services {
    public class DialogService<TDialog, TViewModel> : IDialogService<TDialog, TViewModel>
        where TDialog : Window
        where TViewModel : DialogViewModelBase {

        public TDialog Dialog { get; set; }
        public TViewModel ViewModel { get; set; }

        public DialogService(TDialog dialog, TViewModel viewModel) {
            Dialog = dialog;
            ViewModel = viewModel;
        }

        public void Initialize() {
            Dialog.DataContext = ViewModel;
            ViewModel.RequestCloseDialog += (sender, e) => Dialog.DialogResult = true;
        }

        public bool? ShowDialog() {
            return Dialog?.ShowDialog();
        }
    }
}
