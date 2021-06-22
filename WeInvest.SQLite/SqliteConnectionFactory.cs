using System.Configuration;
using System.Data.SQLite;

namespace WeInvest.SQLite {
    public class SQLiteConnectionFactory {

        public string ConnectionStringId { get; set; }

        public SQLiteConnectionFactory(string connectionStringId) {
            ConnectionStringId = connectionStringId;
        }

        public SQLiteConnection Create() {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings[ConnectionStringId].ConnectionString);
        }
    
    }
}
