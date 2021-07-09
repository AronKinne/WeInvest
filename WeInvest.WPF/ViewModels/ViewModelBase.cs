using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WeInvest.WPF.Commands;

namespace WeInvest.WPF.ViewModels {
    public class ViewModelBase : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public ICommand DragCommand { get; }

        public ViewModelBase() {
            DragCommand = new RelayCommand(p => ((Window)p).DragMove());
        }

    }
}
