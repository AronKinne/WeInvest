using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WeInvest.WPF.Utilities;

namespace WeInvest.WPF.Commands {
    public class AsyncRelayCommand : ICommand {

        private Func<object, Task> _execute;
        private Predicate<object> _canExecute;
        private object _canExecuteParameter;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<object, Task> execute) : this(execute, null) { }

        public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute) {
            if(execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            _canExecuteParameter = parameter;
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(object parameter) {
            ExecuteAsync(parameter).FireAndForget();
        }

        public async Task ExecuteAsync(object parameter) {
            if(!CanExecute(_canExecuteParameter))
                return;

            try {
                _isExecuting = true;
                await _execute(parameter);
            } finally {
                _isExecuting = false;
                OnCanExecuteChanged();
            }
        }

        public void OnCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
