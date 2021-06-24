using Microsoft.Extensions.DependencyInjection;
using System;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.SQLite.Services;

namespace WeInvest.WPF.Utilities {
    public class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IListConvertingService, ListConvertingService>();
            services.AddSingleton<IBrushConvertingService, BrushConvertingService>();

            services.AddSingleton<IDataService<Investor>, InvestorDataService>();

            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<InvestorGroup>, InvestorGroupFactory>();

            return services.BuildServiceProvider();
        }

    }
}
