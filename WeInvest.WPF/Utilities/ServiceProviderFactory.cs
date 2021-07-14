using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.SQLite.DataAccess;
using WeInvest.SQLite.Factories;
using WeInvest.SQLite.Services;
using WeInvest.WPF.Commands;
using WeInvest.WPF.Services;
using WeInvest.WPF.State.Accounts;
using WeInvest.WPF.State.Investors;
using WeInvest.WPF.State.Navigators;
using WeInvest.WPF.State.Stocks;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Controls;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.ViewModels.Dialogs.Factories;
using WeInvest.WPF.ViewModels.Factories;
using WeInvest.WPF.Views;
using WeInvest.WPF.Views.Dialogs;
using WeInvest.WPF.Views.Dialogs.Factories;

namespace WeInvest.WPF.Utilities {
    public static class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            // Views
            services.AddScoped<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
            services.AddSingleton<IFactory<InvestorDialog>, InvestorDialogFactory>();
            services.AddSingleton<IFactory<DepositDialog>, DepositDialogFactory>();

            // ViewModels
            services.AddScoped<MainViewModel>();
            services.AddScoped<HomeViewModel>();
            services.AddScoped<TabBarViewModel>();
            services.AddScoped<DisplayedAccountPieChartViewModel>();
            services.AddScoped<AccountsAreaChartViewModel>();
            services.AddScoped<InvestorLineChartsViewModel>();
            services.AddSingleton<IFactory<InvestorDialogViewModel>, InvestorDialogViewModelFactory>();
            services.AddSingleton<IFactory<DepositDialogViewModel>, DepositDialogViewModelFactory>();
            services.AddSingleton<IFactory<StockViewModel>, StockViewModelFactory>();

            // State
            services.AddSingleton<IInvestorsStore, InvestorsStore>();
            services.AddSingleton<IAccountsStore, AccountsStore>();
            services.AddSingleton<IDisplayedAccountStore, DisplayedAccountStore>();
            services.AddSingleton<IStocksStore, StocksStore>();
            services.AddSingleton<INavigator, Navigator>();

            // Converters
            services.AddSingleton<IListStringConverter, ListStringConverter>();
            services.AddSingleton<IBrushStringConverter, BrushStringConverter>();
            services.AddSingleton<IDictionaryStringConverter, DictionaryStringConverter>();

            // Services
            services.AddSingleton<DialogServiceFactory<InvestorDialog, InvestorDialogViewModel>>();
            services.AddSingleton<DialogServiceFactory<DepositDialog, DepositDialogViewModel>>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IQueryService, DapperQueryService>();

            // Data Access
            services.AddSingleton<IDataAccess<Investor>, InvestorDataAccess>();
            services.AddSingleton<IDataAccess<Account>, AccountDataAccess>();
            services.AddSingleton<IDataAccess<Stock>, StockDataAccess>();

            // Factories
            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<Account>, AccountFactory>();
            services.AddSingleton<IFactory<Stock>, StockFactory>();
            services.AddSingleton<IFactory<IDbConnection>, SQLiteConnectionFactory>();

            // Commands
            services.AddScoped<AddInvestorAsyncCommand>();
            services.AddScoped<RemoveInvestorAsyncCommand>();
            services.AddScoped<DepositAsyncCommand>();
            services.AddScoped<SetDisplayedAccountCommand>();

            return services.BuildServiceProvider();
        }

    }
}
