using System;
using System.Windows.Input;
using WeInvest.WPF.Commands;

namespace WeInvest.WPF.ViewModels.Dialogs {
    public abstract class DialogViewModelBase : ViewModelBase {

        public event EventHandler RequestCloseDialog;

        public string OkayButtonContent { get; set; } = "Okay";
        public ICommand OkayButtonCommand { get; protected set; }

        public DialogViewModelBase() {
            this.OkayButtonCommand = new RelayCommand(Okay, CanOkay);
        }

        protected virtual void OnRequestCloseDialog() {
            RequestCloseDialog?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void Okay(object parameter) {
            OnRequestCloseDialog();
        }

        protected virtual bool CanOkay(object parameter) {
            return true;
        }

    }
}
