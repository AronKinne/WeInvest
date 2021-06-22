using Dapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeInvest.Utilities.Services;

namespace WeInvest.SQLite.Services {
    public abstract class GenericDataService<T> : IDataService<T> {

        private readonly SQLiteConnectionFactory _connectionFactory;
        private readonly string _tableName;
        private readonly IEnumerable<PropertyInfo> _usedProperties;

        protected GenericDataService(string tableName, string connectionStringId, IEnumerable<PropertyInfo> usedProperties) {
            _tableName = tableName;
            _connectionFactory = new SQLiteConnectionFactory(connectionStringId);
            _usedProperties = usedProperties;
        }

        public async Task<T> CreateAsync(T entity) {
            using(var connection = _connectionFactory.Create()) {
                return await connection.QuerySingleAsync<T>($"{GenerateCreateQuery()}; SELECT * FROM {_tableName} WHERE Id = last_insert_rowid();", entity);
            }
        }

        public async Task DeleteAsync(int id) {
            using(var connection = _connectionFactory.Create()) {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<T> GetAsync(int id) {
            using(var connection = _connectionFactory.Create()) {
                var output = await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });

                if(output == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return output;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            using(var connection = _connectionFactory.Create()) {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }

        public async Task<T> UpdateAsync(int id, T entity) {
            using(var connection = _connectionFactory.Create()) {
                return await connection.QuerySingleAsync<T>($"{GenerateUpdateQuery(id)}; SELECT * FROM {_tableName} WHERE Id = {id}", entity);
            }
        }


        protected List<string> GeneratePropertyList() {
            return (from property in _usedProperties
                    let attributes = property.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select property.Name).ToList();
        }

        protected string GenerateCreateQuery() {
            var query = new StringBuilder($"INSERT INTO {_tableName} (");

            var properties = GeneratePropertyList();
            properties.ForEach(p => query.Append($"{p}, "));

            query
                .Remove(query.Length - 2, 2)
                .Append(") VALUES (");

            properties.ForEach(p => query.Append($"@{p}, "));

            query
                .Remove(query.Length - 2, 2)
                .Append(")");

            return query.ToString();
        }

        protected string GenerateUpdateQuery(int id) {
            var query = new StringBuilder($"UPDATE {_tableName} SET ");

            foreach(var property in GeneratePropertyList()) {
                if(property != "Id")
                    query.Append($"{property} = @{property}, ");
            }

            query
                .Remove(query.Length - 2, 2)
                .Append($" WHERE Id = {id}");

            return query.ToString();
        }

    }
}
