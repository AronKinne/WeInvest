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
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Utilities {
    public static class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            // Views
            services.AddScoped<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
            services.AddTransient<DepositDialog>();
            services.AddTransient<InvestorDialog>();

            // ViewModels
            services.AddScoped<MainViewModel>();
            services.AddScoped<DepositDialogViewModel>();
            services.AddScoped<InvestorDialogViewModel>();

            // State
            services.AddSingleton<IInvestorsStore, InvestorsStore>();
            services.AddSingleton<IAccountsStore, AccountsStore>();

            // Converters
            services.AddSingleton<IListStringConverter, ListStringConverter>();
            services.AddSingleton<IBrushStringConverter, BrushStringConverter>();
            services.AddSingleton<IDictionaryStringConverter, DictionaryStringConverter>();

            // Services
            services.AddTransient<IDialogService<DepositDialog, DepositDialogViewModel>, DialogService<DepositDialog, DepositDialogViewModel>>();
            services.AddTransient<IDialogService<InvestorDialog, InvestorDialogViewModel>, DialogService<InvestorDialog, InvestorDialogViewModel>>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IQueryService, DapperQueryService>();

            // Data Access
            services.AddSingleton<IDataAccess<Investor>, InvestorDataAccess>();
            services.AddSingleton<IDataAccess<Account>, AccountDataAccess>();

            // Factories
            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<Account>, AccountFactory>();
            services.AddSingleton<IFactory<IDbConnection>, SQLiteConnectionFactory>();

            // Commands
            services.AddScoped<AddInvestorCommand>();
            services.AddScoped<DepositCommand>();

            return services.BuildServiceProvider();
        }

    }
}
