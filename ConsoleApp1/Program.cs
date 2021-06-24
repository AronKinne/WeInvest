using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.SQLite.Services;

namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName);

            IDataService<Investor> investorService = new InvestorDataService();
            var investorFactory = new InvestorFactory(new ListStringConverter(), new BrushStringConverter());

            // CREATE
            //Console.WriteLine(investorService.CreateAsync(investorFactory.Create("Tester", Brushes.Black)).Result.Id);

            // UPDATE
            //investorService.UpdateAsync(1, investorFactory.Create("Beta Tester", Brushes.Red)).Wait();

            // GET
            //Console.WriteLine(investorService.GetAsync(1).Result);

            // GETALL
            Console.WriteLine(investorService.GetAllAsync().Result.Count());

            // DELETE
            //investorService.DeleteAsync(1).Wait();
        }
    }
}
