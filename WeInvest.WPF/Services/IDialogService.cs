namespace WeInvest.WPF.Services {
    public interface IDialogService<TDialog, TViewModel> {

        TDialog Dialog { get; }
        TViewModel ViewModel { get; }

        bool? ShowDialog();

    }
}
