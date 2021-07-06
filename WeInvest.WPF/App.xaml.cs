using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.Views;

namespace WeInvest.WPF {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName);

            var serviceProvider = ServiceProviderFactory.Create();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var investorDataAccess = serviceProvider.GetRequiredService<IDataAccess<Investor>>();
            var investorsStore = serviceProvider.GetRequiredService<IInvestorsStore>();
            investorsStore.CurrentInvestors = new ObservableCollection<Investor>(investorDataAccess.GetAllAsync().Result);

            var accountDataAccess = serviceProvider.GetRequiredService<IDataAccess<Account>>();
            var accountsStore = serviceProvider.GetRequiredService<IAccountsStore>();
            accountsStore.CurrentAccounts = new ObservableCollection<Account>(accountDataAccess.GetAllAsync().Result);

            base.OnStartup(e);
        }

    }
}
