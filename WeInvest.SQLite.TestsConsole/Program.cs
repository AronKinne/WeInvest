using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WeInvest.Domain.Converters;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Models;
using WeInvest.Domain.Services;
using WeInvest.SQLite.DataAccess;
using WeInvest.SQLite.Factories;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.TestsConsole {
    class Program {
        static void Main(string[] args) {
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName);

            #region Investor

            var connectionFactory = new SQLiteConnectionFactory();
            var queryService = new DapperQueryService();
            var investorFactory = new InvestorFactory(new ListStringConverter(), new BrushStringConverter());
            IDataAccess<Investor> investorService = new InvestorDataAccess(connectionFactory, queryService, investorFactory);

            // CREATE
            //Console.WriteLine(investorService.CreateAsync(investorFactory.Create(new { Name = "Tester", Brush = Brushes.Black })).Result.Id);

            // UPDATE
            //investorService.UpdateAsync(3, investorFactory.Create(new { Name = "Beta Tester", Brush = Brushes.Red })).Wait();

            // GET
            //Console.WriteLine(investorService.GetAsync(3).Result);

            // GETALL
            //Console.WriteLine(investorService.GetAllAsync().Result.Count());

            // DELETE
            //investorService.DeleteAsync(3).Wait();

            #endregion

            #region Account

            IDataAccess<Account> accountService = new AccountDataAccess(connectionFactory, queryService, new AccountFactory(new DictionaryStringConverter(new ListStringConverter()), investorService));
            var accountFactory = new AccountFactory(new DictionaryStringConverter(new ListStringConverter()), investorService);

            // CREATE
            //Console.WriteLine(accountService.CreateAsync(accountFactory.Create()).Result.Id);

            // UPDATE
            //accountService.UpdateAsync(1, accountFactory.Create(new { ShareByInvestorString = "1|50 2|30" })).Wait();

            // GET
            //Console.WriteLine(accountService.GetAsync(1).Result.ShareByInvestorString);

            // GETALL
            //Console.WriteLine(accountService.GetAllAsync().Result.Count());

            // DELETE
            //accountService.DeleteAsync(1).Wait();

            #endregion
        }
    }
}
