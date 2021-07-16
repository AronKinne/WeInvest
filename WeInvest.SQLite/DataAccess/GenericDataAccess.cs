using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeInvest.Domain.Factories;
using WeInvest.Domain.Services;
using WeInvest.SQLite.Services;

namespace WeInvest.SQLite.DataAccess {
    public abstract class GenericDataAccess<T> : IDataAccess<T> {

        protected readonly IFactory<IDbConnection> _connectionFactory;
        protected readonly IQueryService _queryService;
        protected readonly string _connectionStringId;
        protected readonly string _tableName;
        protected readonly IEnumerable<PropertyInfo> _usedProperties;

        protected GenericDataAccess(
            IFactory<IDbConnection> connectionFactory,
            IQueryService queryService,
            string connectionStringId,
            string tableName,
            IEnumerable<PropertyInfo> usedProperties) {

            _connectionFactory = connectionFactory;
            _queryService = queryService;
            _connectionStringId = connectionStringId;
            _tableName = tableName;
            _usedProperties = usedProperties;
        }

        public async Task<T> CreateAsync(T entity) {
            using(var connection = _connectionFactory.Create(_connectionStringId)) {
                var created = await _queryService.QuerySingleAsync(connection, $"{GenerateCreateQuery()}; SELECT * FROM {_tableName} WHERE Id = last_insert_rowid();", entity);
                return CreateEntity(created);
            }
        }

        public async Task DeleteAsync(int id) {
            using(var connection = _connectionFactory.Create(_connectionStringId)) {
                await _queryService.ExecuteAsync(connection, $"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<T> GetAsync(int id) {
            using(var connection = _connectionFactory.Create(_connectionStringId)) {
                var result = await _queryService.QueryFirstOrDefaultAsync(connection, $"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });

                if(result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return CreateEntity(result);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() {
            using(var connection = _connectionFactory.Create(_connectionStringId)) {
                var rows = await _queryService.QueryAsync(connection, $"SELECT * FROM {_tableName}");
                return CreateEntityEnumerable(rows);
            }
        }

        public async Task<T> UpdateAsync(int id, T entity) {
            using(var connection = _connectionFactory.Create(_connectionStringId)) {
                var result = await _queryService.QuerySingleAsync(connection, $"{GenerateUpdateQuery(id)}; SELECT * FROM {_tableName} WHERE Id = {id}", entity);
                return CreateEntity(result);
            }
        }

        protected abstract T CreateEntity(dynamic dynamicObject);

        private IEnumerable<T> CreateEntityEnumerable(IEnumerable<dynamic> rows) {
            var output = new List<T>();
            foreach(var row in rows)
                output.Add(CreateEntity(row));
            return output;
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
