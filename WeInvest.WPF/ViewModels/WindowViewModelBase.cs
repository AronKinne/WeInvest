using System.Windows;
using System.Windows.Input;
using WeInvest.WPF.Commands;

namespace WeInvest.WPF.ViewModels {
    public abstract class WindowViewModelBase : ViewModelBase {

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }

        public WindowViewModelBase() {
            CloseCommand = new RelayCommand(p => ((Window)p).Close());
            MinimizeCommand = new RelayCommand(p => ((Window)p).WindowState = WindowState.Minimized);
        }

    }
}
