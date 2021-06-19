using Microsoft.Extensions.DependencyInjection;
using System;
using WeInvest.Models;
using WeInvest.Utilities.Services;

namespace WeInvest.Utilities.Factories {
    public class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IListConvertingService, ListConvertingService>();
            services.AddSingleton<IBrushConvertingService, BrushConvertingService>();

            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<InvestorGroup>, InvestorGroupFactory>();

            return services.BuildServiceProvider();
        }

    }
}
