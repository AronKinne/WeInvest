using System;

namespace WeInvest.ViewModels.Dialogs {
    abstract class DialogViewModelBase {

        public event EventHandler RequestCloseDialog;

        public DialogViewModelBase() {

        }

        protected virtual void OnRequestCloseDialog() {
            RequestCloseDialog?.Invoke(this, EventArgs.Empty);
        }

    }
}
