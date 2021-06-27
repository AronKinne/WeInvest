using System.Data;
using System.Threading.Tasks;

namespace WeInvest.SQLite.Services {
    public interface IQueryService {

        Task<dynamic> QueryAsync(IDbConnection connection, string sql, object parameter = null);
        Task<dynamic> QueryFirstOrDefaultAsync(IDbConnection connection, string sql, object parameter = null);
        Task<dynamic> QuerySingleAsync(IDbConnection connection, string sql, object parameter = null);
        Task<int> ExecuteAsync(IDbConnection connection, string sql, object parameter = null);

    }
}
