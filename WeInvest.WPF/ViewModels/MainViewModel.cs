using System.Windows;
using System.Windows.Input;
using WeInvest.WPF.Commands;
using WeInvest.WPF.State.Navigators;
using WeInvest.WPF.ViewModels.Controls;

namespace WeInvest.WPF.ViewModels {
    public class MainViewModel : WindowViewModelBase {

        private readonly INavigator _navigator;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand MinimizeCommand { get; }
        public TabBarViewModel TabBarViewModel { get; }

        public MainViewModel(INavigator navigator, TabBarViewModel tabBarViewModel) : base() {
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;

            MinimizeCommand = new RelayCommand(p => ((Window)p).WindowState = WindowState.Minimized);
            TabBarViewModel = tabBarViewModel;
        }

        private void Navigator_StateChanged(object sender, System.EventArgs e) {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
