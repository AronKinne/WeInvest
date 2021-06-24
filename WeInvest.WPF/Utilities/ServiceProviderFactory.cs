﻿using Microsoft.Extensions.DependencyInjection;
using System;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.SQLite.Services;

namespace WeInvest.WPF.Utilities {
    public class ServiceProviderFactory {
    
        public static IServiceProvider Create() {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IListStringConverter, ListStringConverter>();
            services.AddSingleton<IBrushStringConverter, BrushStringConverter>();

            services.AddSingleton<IDataService<Investor>, InvestorDataService>();

            services.AddSingleton<IFactory<Investor>, InvestorFactory>();
            services.AddSingleton<IFactory<InvestorGroup>, InvestorGroupFactory>();

            return services.BuildServiceProvider();
        }

    }
}
