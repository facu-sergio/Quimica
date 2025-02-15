using Core.Models.Options;
using Dapper;
using Microsoft.Data.SqlClient;
using Quimica.Core.attributes;
using Quimica.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Quimica.Service.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public GenericRepository(IConnectionBuilder connectionBuilder)
        {
            _connectionString = connectionBuilder.GetConnectionString();
            _tableName = connectionBuilder.GetTableName<T>();
        }

        public async Task<T> GetByIdAsync(int id, IDbConnection connection = null)
        {
            connection ??= new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<T>(
                $"SELECT * FROM {_tableName} WHERE Id = @Id",
                new { Id = id }
            );
        }

        public async Task<IEnumerable<T>> GetAllAsync(IDbConnection connection = null)
        {
            connection ??= new SqlConnection(_connectionString);

            return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
        }

        public async Task<int> AddAsync(T entity, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            var insertQuery = GenerateInsertQuery();
            connection ??= new SqlConnection(_connectionString);

            return await connection.ExecuteScalarAsync<int>(
                $"{insertQuery}; SELECT CAST(SCOPE_IDENTITY() AS INT);",
                entity,
                transaction
            );
        }

        public async Task UpdateAsync(T entity, string keyProperty = "Id", IDbTransaction transaction = null, IDbConnection connection = null)
        {
            var updateQuery = GenerateUpdateQuery(keyProperty);
            connection ??= new SqlConnection(_connectionString);

            await connection.ExecuteAsync(updateQuery, entity, transaction);
        }

        public async Task DeleteByIdAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            connection ??= new SqlConnection(_connectionString);

            await connection.ExecuteAsync(
                $"DELETE FROM {_tableName} WHERE Id = @Id",
                new { Id = id },
                transaction
            );
        }

        public async Task<int> DeleteAsync(string condition, object parameters, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            connection ??= new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(
                $"DELETE FROM {_tableName} WHERE {condition};", 
                parameters, 
                transaction
            );
        }

        public async Task<T> FindAsync(string columnName, object value, IDbConnection connection = null)
        {
            connection ??= new SqlConnection(_connectionString);

            var query = $"SELECT * FROM {_tableName} WHERE {columnName} = @Value";
            return await connection.QueryFirstOrDefaultAsync<T>(query, new { Value = value });
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(string condition, object parameters = null, IDbConnection connection = null)
        {
            connection ??= new SqlConnection(_connectionString);

            return await connection.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE {condition}", parameters);
        }

        private string GenerateInsertQuery()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" &&
                            !p.PropertyType.IsClass || p.PropertyType == typeof(string)); // Ignorar objetos complejos excepto string

            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            return $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
        }

        private string GenerateUpdateQuery(string keyProperty = "Id")
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != keyProperty && p.Name != "Id" &&
                            !p.IsDefined(typeof(NotUpdatableAttribute), false) &&
                            p.CanRead &&
                            (p.PropertyType.IsPrimitive ||
                             p.PropertyType == typeof(string) ||
                             Nullable.GetUnderlyingType(p.PropertyType) != null));

            var setClauses = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            return $"UPDATE {_tableName} SET {setClauses} WHERE {keyProperty} = @{keyProperty}";
        }
    }
}
