using WeInvest.WPF.ViewModels;

namespace WeInvest.WPF.State.Navigators {
    public interface INavigator {

        ViewModelBase CurrentViewModel { get; set; }

    }
}
