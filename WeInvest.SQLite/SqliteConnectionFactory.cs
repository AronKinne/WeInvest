using Microsoft.Data.Sqlite;

namespace WeInvest.SQLite {
    public class SqliteConnectionFactory {

        public string ConnectionStringId { get; set; }

        public SqliteConnectionFactory(string connectionStringId) {
            ConnectionStringId = connectionStringId;
        }

        public SqliteConnection Create() {
            return new SqliteConnection(ConnectionStringId);
        }
    
    }
}
