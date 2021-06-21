using System.Windows.Media;
using WeInvest.Models;
using WeInvest.SQLite.Services;
using WeInvest.Utilities.Factories;
using WeInvest.Utilities.Services;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {

            IDataService<Investor> investorService = new InvestorDataService();
            var investorFactory = new InvestorFactory(new ListConvertingService(), new BrushConvertingService());

            investorService.CreateAsync(investorFactory.Create("Tester", Brushes.Black)).Wait();

            //System.Console.WriteLine(investorService.CreateAsync(investorFactory.Create("Tester", Brushes.Black)).Result);

            System.Console.ReadKey();

        }
    }
}
