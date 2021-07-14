using WeInvest.WPF.ViewModels;

namespace WeInvest.WPF.State.Navigators {
    public class Navigator : INavigator {

        public ViewModelBase CurrentViewModel { get; set; }

    }
}
