using System.Windows;
using WeInvest.Domain.Factories;
using WeInvest.WPF.ViewModels.Dialogs;

namespace WeInvest.WPF.Services {
    public class DialogServiceFactory<TDialog, TViewModel>
        where TDialog : Window
        where TViewModel : DialogViewModelBase {

        private readonly IFactory<TDialog> _dialogFactory;
        private readonly IFactory<TViewModel> _viewModelFactory;

        public DialogServiceFactory(IFactory<TDialog> dialogFactory, IFactory<TViewModel> viewModelFactory) {
            _dialogFactory = dialogFactory;
            _viewModelFactory = viewModelFactory;
        }

        public DialogService<TDialog, TViewModel> CreateAndInitialize() {
            var dialog = _dialogFactory.Create();
            var viewModel = _viewModelFactory.Create();

            var service = new DialogService<TDialog, TViewModel>(dialog, viewModel);
            service.Initialize();
            return service;
        }

    }
}
