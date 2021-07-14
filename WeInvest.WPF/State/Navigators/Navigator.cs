using System;
using WeInvest.WPF.ViewModels;

namespace WeInvest.WPF.State.Navigators {
    public class Navigator : INavigator {

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnStateChanged();
            }
        }

        public event EventHandler StateChanged;

        private void OnStateChanged() {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
