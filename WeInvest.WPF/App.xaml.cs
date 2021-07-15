using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.State.Navigators;
using WeInvest.WPF.State.Stocks;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.ViewModels;
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
            investorsStore.Investors = new ObservableCollection<Investor>(investorDataAccess.GetAllAsync().Result);

            var accountDataAccess = serviceProvider.GetRequiredService<IDataAccess<Account>>();
            var accountsStore = serviceProvider.GetRequiredService<IAccountsStore>();
            accountsStore.Accounts = new ObservableCollection<Account>(accountDataAccess.GetAllAsync().Result);

            var stockDataAccess = serviceProvider.GetRequiredService<IDataAccess<Stock>>();
            var stocksStore = serviceProvider.GetRequiredService<IStocksStore>();
            stocksStore.Stocks = new ObservableCollection<Stock>(stockDataAccess.GetAllAsync().Result);

            var initialViewModel = serviceProvider.GetRequiredService<HomeViewModel>();
            var navigator = serviceProvider.GetRequiredService<INavigator>();
            navigator.CurrentViewModel = initialViewModel;

            base.OnStartup(e);
        }

    }
}
