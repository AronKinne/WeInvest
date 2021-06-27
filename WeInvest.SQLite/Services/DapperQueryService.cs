using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace WeInvest.SQLite.Services {
    public class DapperQueryService : IQueryService {

        public async Task<int> ExecuteAsync(IDbConnection connection, string sql, object parameter = null) {
            return await connection.ExecuteAsync(sql, parameter);
        }

        public async Task<dynamic> QueryAsync(IDbConnection connection, string sql, object parameter = null) {
            return await connection.QueryAsync(sql, parameter);
        }

        public async Task<dynamic> QueryFirstOrDefaultAsync(IDbConnection connection, string sql, object parameter = null) {
            return await connection.QueryFirstOrDefaultAsync(sql, parameter);
        }

        public async Task<dynamic> QuerySingleAsync(IDbConnection connection, string sql, object parameter = null) {
            return await connection.QuerySingleAsync(sql, parameter);
        }

    }
}
