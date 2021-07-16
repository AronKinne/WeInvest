using System;
using System.Windows;
using System.Windows.Input;
using WeInvest.WPF.Commands;

namespace WeInvest.WPF.ViewModels.Dialogs {
    public abstract class DialogViewModelBase : WindowViewModelBase {

        public event EventHandler RequestCloseDialog;

        public string OkayButtonContent { get; set; } = "Okay";
        public ICommand OkayButtonCommand { get; protected set; }
        public ICommand FocusElementCommand { get; protected set; }

        public DialogViewModelBase() {
            OkayButtonCommand = new RelayCommand(Okay, CanOkay);
            FocusElementCommand = new RelayCommand(p => ((UIElement)p).Focus());
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
