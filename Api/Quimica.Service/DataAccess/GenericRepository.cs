using Dapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Quimica.Core.Attributes;
using Quimica.Core.DataAccess;
using Quimica.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DapperDataContext _dapperDataContext;

          public GenericRepository(DapperDataContext dapperDataContext)
        {
            _dapperDataContext = dapperDataContext;
        }


        public Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters, params string[] selectData)
        {
            throw new NotImplementedException();
        }
        public async Task<T> GetByIdAsync(int id, params string[] selectData)
        {

            using (var connection = _dapperDataContext.Connection)
            {
                string tableName = typeof(T).GetDbTableName();

                string columns = selectData != null && selectData.Length > 0
                    ? string.Join(", ", selectData)
                    : "*";

                string query = $"SELECT {columns} FROM {tableName} WHERE Id = @id";

                if (_dapperDataContext.Transaction != null)
                {
                    return await connection.QuerySingleOrDefaultAsync<T>(query, new { id }, transaction: _dapperDataContext.Transaction);
                }
                else
                {
                    return await connection.QuerySingleOrDefaultAsync<T>(query, new { id });
                }

            }
        }

        public async Task<int> AddAsync(T entity)
        {
            var connection = _dapperDataContext.Connection; // Usa la conexión existente
            var transaction = _dapperDataContext.Transaction; // Usa la transacción activa

            string tableName = typeof(T).GetDbTableName();

            var properties = typeof(T)
                .GetNonPrimaryKeyColumnProperties()
                .ToList();

            var columns = properties.Select(p => $"[{p.GetDbColumnName()}]");
            var parameters = new DynamicParameters();

            var paramNames = properties.Select(p =>
            {
                var paramName = $"@{p.Name}";
                parameters.Add(paramName, p.GetValue(entity));
                return paramName;
            });

            string query =   $@"INSERT INTO {tableName} ({string.Join(", ", columns)}) 
                                VALUES ({string.Join(", ", paramNames)}); 
                                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await connection.ExecuteScalarAsync<int>(query, parameters, transaction: transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            string tableName = typeof(T).GetDbTableName();
            var parametersToUpdate = new DynamicParameters();

            var properties = typeof(T)
               .GetNonPrimaryKeyColumnProperties()
               .ToList();

            var setClause = new List<string>();
            
            foreach (var p in properties)
            {
                setClause.Add($"{p.Name} = @{p.Name}");
                var paramName = $"@{p.Name}";
                parametersToUpdate.Add(paramName, p.GetValue(entity));
            }

            var idProperty = typeof(T).GetProperty("Id");
            var idValue = idProperty.GetValue(entity);
            parametersToUpdate.Add("@id", idValue);

            string query = $"UPDATE {tableName} SET {string.Join(",", setClause)} WHERE Id = @id";

            await _dapperDataContext.Connection.ExecuteAsync(query, parametersToUpdate, transaction: _dapperDataContext.Transaction);
        }


        public async Task<IEnumerable<T>> GetBySpecificColumnAsync(string columnName, object columnValue, params string[] selectData)
        {


            using (var connection = _dapperDataContext.Connection)
            {
                string tableName = typeof(T).GetDbTableName();

                string columns = selectData != null && selectData.Length > 0
                    ? string.Join(", ", selectData)
                    : "*";

                string query;

                if (columnValue is List<int> values) 
                {
                    query = $"SELECT {columns} FROM {tableName} WHERE {columnName} IN @values";
                    return await connection.QueryAsync<T>(query, new { values });
                }
                else
                {
                    query = $"SELECT {columns} FROM {tableName} WHERE {columnName} = @columnValue";
                    return await connection.QueryAsync<T>(query, new { columnValue });
                }

            }
        }

        public async Task SoftDeleteAsync(int id, bool softDeleteFromRelatedChildTables = false)
        {
            var connection = _dapperDataContext.Connection;
            var transaction = _dapperDataContext.Transaction;

            // 1. Obtener nombre de la tabla y columna para soft delete
            string tableName = typeof(T).GetDbTableName();
            string softDeleteColumn = "is_deleted"; // Columna por defecto

            // 2. Buscar atributo personalizado o propiedad (opcional)
            var softDeleteProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<SoftDeleteColumnAttribute>() != null);

            if (softDeleteProperty != null)
            {
                softDeleteColumn = softDeleteProperty.GetDbColumnName();
            }

            // 3. Query base
            string query = $@"UPDATE {tableName} 
                     SET {softDeleteColumn} = 1,
                     delete_at = GETDATE()
                     WHERE Id = @id";

            // 4. Ejecutar
            await connection.ExecuteAsync(query, new { id }, transaction: transaction);
        }

        
        public async Task<IEnumerable<T>> GetAllAsync(params string[] selectData)
        {
            using (var connection = _dapperDataContext.Connection)
            {
                string tableName = typeof(T).GetDbTableName();
                
                string columns = selectData != null && selectData.Length > 0
                    ? string.Join(", ", selectData)
                    : "*";
                
                string query = $"SELECT {columns} FROM {tableName} WHERE is_deleted = 0 OR is_deleted IS NULL";
                
                return await connection.QueryAsync<T>(query, transaction: _dapperDataContext.Transaction);
            }
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetByFilterAsync(Dictionary<string, object> filters, params string[] selectData)
        {
            using (var connection = _dapperDataContext.Connection)
            {
                string tableName = typeof(T).GetDbTableName();

                string columns = selectData != null && selectData.Length > 0
                    ? string.Join(", ", selectData)
                    : "*";

                var whereConditions = new List<string>();
                var parameters = new DynamicParameters();

                foreach (var filter in filters)
                {
                    if (filter.Value is IEnumerable<int> intValues)
                    {
                        whereConditions.Add($"{filter.Key} IN @{filter.Key}");
                        parameters.Add($"@{filter.Key}", intValues);
                    }
                    else
                    {
                        whereConditions.Add($"{filter.Key} = @{filter.Key}");
                        parameters.Add($"@{filter.Key}", filter.Value);
                    }
                }

                whereConditions.Add("(is_deleted = 0 OR is_deleted IS NULL)");

                string whereClause = $"WHERE {string.Join(" AND ", whereConditions)}";

                string query = $"SELECT {columns} FROM {tableName} {whereClause}";

                return await connection.QueryAsync<T>(query, parameters, transaction: _dapperDataContext.Transaction);
            }
        }
    }
}
