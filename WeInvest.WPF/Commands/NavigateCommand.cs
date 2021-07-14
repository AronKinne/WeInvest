using System;
using System.Windows.Input;
using WeInvest.WPF.State.Navigators;
using WeInvest.WPF.ViewModels;

namespace WeInvest.WPF.Commands {
    public class NavigateCommand : ICommand {

        private readonly INavigator _navigator;

        public event EventHandler CanExecuteChanged;

        public NavigateCommand(INavigator navigator) {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var viewModel = parameter as ViewModelBase;
            _navigator.CurrentViewModel = viewModel;
        }
    }
}
