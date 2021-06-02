﻿using System;
using WeInvest.ViewModels.Commands;

namespace WeInvest.ViewModels.Dialogs {
    public abstract class DialogViewModelBase {

        public event EventHandler RequestCloseDialog;

        public string OkayButtonContent { get; set; } = "Okay";
        public RelayCommand OkayButtonCommand { get; protected set; }

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
