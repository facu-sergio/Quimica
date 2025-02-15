using Core.Models.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Quimica.Core.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Quimica.Service.DataAccess
{
    public  class ConnectionBuilder : IConnectionBuilder
    {
        private readonly DbOptions _options;

        public ConnectionBuilder(IOptions<DbOptions> options)
        {
            _options = options.Value;
        }

        public SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(_options.ConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetConnectionString() => _options.ConnectionString;
        public string GetTableName<T>()
        {
            // Lógica para obtener el nombre de la tabla (ej: usando atributos o convenciones)
            var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
            return tableAttribute?.Name ?? typeof(T).Name; // Ej: "Product" → "Products"
        }
    }
}
