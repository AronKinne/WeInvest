using System;

namespace WeInvest.ViewModels.Dialogs {
    public abstract class DialogViewModelBase {

        public event EventHandler RequestCloseDialog;

        public DialogViewModelBase() {

        }

        protected virtual void OnRequestCloseDialog() {
            RequestCloseDialog?.Invoke(this, EventArgs.Empty);
        }

    }
}
