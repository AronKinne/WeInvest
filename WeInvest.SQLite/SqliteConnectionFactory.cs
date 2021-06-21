using Microsoft.Data.Sqlite;
using System.Configuration;

namespace WeInvest.SQLite {
    public class SqliteConnectionFactory {

        public string ConnectionStringId { get; set; }

        public SqliteConnectionFactory(string connectionStringId) {
            ConnectionStringId = connectionStringId;
        }

        public SqliteConnection Create() {
            var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings[ConnectionStringId].ConnectionString);
            //connection.Open();
            return connection;
        }
    
    }
}
