using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using WeInvest.ViewModels.Dialogs;
using WeInvest.ViewModels.Utilities;
using WeInvest.Views.Dialogs;

namespace WeInvest.ViewModels {
    class MainWindowViewModel {

        public ObservableCollection<KeyValuePair<Brush, float>> PieSeries { get; set; }

        #region Command Properties

        public RelayCommand DepositCommand { get; set; }

        #endregion

        public MainWindowViewModel() {
            this.PieSeries = new ObservableCollection<KeyValuePair<Brush, float>>();
            PieSeries.Add(new KeyValuePair<Brush, float>(Brushes.Coral, 15));
            PieSeries.Add(new KeyValuePair<Brush, float>(Brushes.CornflowerBlue, 85));

            #region Command Initializations

            this.DepositCommand = new RelayCommand(Deposit);

            #endregion
        }

        #region Commands

        private void Deposit(object parameter) {
            var dialogService = new DialogService<DepositDialog, DepositDialogViewModel>();

            if(dialogService.ShowDialog() == true) {
                System.Console.WriteLine();
            }
        }

        #endregion

    }
}
