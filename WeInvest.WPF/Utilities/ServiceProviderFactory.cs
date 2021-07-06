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
using WeInvest.WPF.Commands.Builders;
using WeInvest.WPF.Services;
using WeInvest.WPF.ViewModels;
using WeInvest.WPF.ViewModels.Dialogs;
using WeInvest.WPF.Views;
using WeInvest.WPF.Views.Dialogs;

namespace WeInvest.WPF.Utilities {
    public static class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            // Views
            services.AddScoped<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainWindowViewModel>() });
            services.AddTransient<DepositDialog>();
            services.AddTransient<InvestorDialog>();

            // ViewModels
            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<DepositDialogViewModel>();
            services.AddScoped<InvestorDialogViewModel>();

            // Converters
            services.AddSingleton<IListStringConverter, ListStringConverter>();
            services.AddSingleton<IBrushStringConverter, BrushStringConverter>();
            services.AddSingleton<IDictionaryStringConverter, DictionaryStringConverter>();

            // Services
            services.AddSingleton<IDialogService<DepositDialog, DepositDialogViewModel>, DialogService<DepositDialog, DepositDialogViewModel>>();
            services.AddSingleton<IDialogService<InvestorDialog, InvestorDialogViewModel>, DialogService<InvestorDialog, InvestorDialogViewModel>>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IQueryService, DapperQueryService>();

            // Data Access
            services.AddSingleton<IDataAccess<Investor>, InvestorDataAccess>();
            services.AddSingleton<IDataAccess<Account>, AccountDataAccess>();

            // Factories
            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<InvestorGroup>, InvestorGroupFactory>();
            services.AddSingleton<IFactory<Account>, AccountFactory>();
            services.AddSingleton<IFactory<IDbConnection>, SQLiteConnectionFactory>();

            // Builder
            services.AddSingleton<IBuilder<DepositCommand>, DepositCommandBuilder>();
            services.AddSingleton<IBuilder<AddInvestorCommand>, AddInvestorCommandBuilder>();

            return services.BuildServiceProvider();
        }

    }
}
