using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.Commands;
using WeInvest.WPF.Commands.Builders;
using WeInvest.WPF.Services;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.ViewModels.Controls;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.ViewModels {
    public class MainWindowViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        private readonly IFactory<InvestorGroup> _investorGroupFactory;

        private readonly IDataAccess<Investor> _investorDataAccess;

        private readonly IBuilder<DepositCommand> _depositCommandBuilder;
        private readonly IBuilder<AddInvestorCommand> _addInvestorCommandBuilder;

        public ICommand DepositCommand { get; }
        public ICommand AddInvestorCommand { get; }


        public InvestorGroup InvestorGroup { get; set; }
        public IList<Investor> Investors { get => new List<Investor>(InvestorGroup?.Investors); }

        public MainAccountPieControlViewModel MainAccountPieViewModel { get; set; }
        public MainAccountAreaControlViewModel MainAccountAreaViewModel { get; set; }
        public InvestorChartControlViewModel InvestorChartViewModel { get; set; }

        public MainWindowViewModel(IFactory<InvestorGroup> investorGroupFactory, IFactory<Investor> investorFactory, IDataAccess<Investor> investorDataAccess, IDialogService<DepositDialog, DepositDialogViewModel> despositDialogService, IDialogService<InvestorDialog, InvestorDialogViewModel> investorDialogService) {
            _investorGroupFactory = investorGroupFactory;

            _investorDataAccess = investorDataAccess;

            _depositCommandBuilder = new DepositCommandBuilder(this, despositDialogService).Build();
            DepositCommand = _depositCommandBuilder.Get();

            _addInvestorCommandBuilder = new AddInvestorCommandBuilder(this, investorDialogService, investorFactory, investorDataAccess).Build();
            AddInvestorCommand = _addInvestorCommandBuilder.Get();

            InvestorGroup = _investorGroupFactory.Create();

            MainAccountPieViewModel = new MainAccountPieControlViewModel(InvestorGroup);
            MainAccountAreaViewModel = new MainAccountAreaControlViewModel(InvestorGroup) { AreaOpacity = 1 };
            InvestorChartViewModel = new InvestorChartControlViewModel(InvestorGroup);

            foreach(var investor in _investorDataAccess.GetAllAsync().Result) {
                InvestorGroup.AddInvestor(investor);
            }

            //Deposit(stefan, 15);
            //Deposit(aron, 85);
            //Deposit(stefan, 50);
        }
    }
}
