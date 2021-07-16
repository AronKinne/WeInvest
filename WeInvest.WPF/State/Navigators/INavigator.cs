using WeInvest.WPF.ViewModels;

namespace WeInvest.WPF.State.Navigators {
    public interface INavigator : IStore {

        ViewModelBase CurrentViewModel { get; set; }

    }
}
