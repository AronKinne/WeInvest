﻿using System;
using System.Windows.Input;

namespace WeInvest.ViewModels.Utilities {
    class RelayCommand : ICommand {

        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> predicate) {
            if(execute == null)
                throw new ArgumentNullException("execute");

            this._execute = execute;
            this._canExecute = predicate;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }
    }
}
