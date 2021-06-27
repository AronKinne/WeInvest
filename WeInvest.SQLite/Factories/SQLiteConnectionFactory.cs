using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using WeInvest.Domain.Factories;

namespace WeInvest.SQLite.Factories {
    public class SQLiteConnectionFactory : IFactory<IDbConnection> {

        public IDbConnection Create() {
            throw new NotImplementedException();
        }

        public IDbConnection Create(object parameter) {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings[parameter.ToString()].ConnectionString);
        }
    }
}
