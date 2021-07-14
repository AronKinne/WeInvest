using System.Windows;
using System.Windows.Input;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Navigators;

namespace WeInvest.WPF.ViewModels {
    public class MainViewModel : WindowViewModelBase {

        private readonly INavigator _navigator;

        public ICommand MinimizeCommand { get; }
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public MainViewModel(INavigator navigator) : base() {
            _navigator = navigator;
            MinimizeCommand = new RelayCommand(p => ((Window)p).WindowState = WindowState.Minimized);
        }

    }
}
