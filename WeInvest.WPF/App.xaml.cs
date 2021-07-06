using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using WeInvest.WPF.Utilities;
using WeInvest.WPF.Views;

namespace WeInvest.WPF {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName);

            var serviceProvider = ServiceProviderFactory.Create();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

    }
}
