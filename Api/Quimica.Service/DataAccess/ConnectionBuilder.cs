using Core.Models.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Quimica.Core.DataAccess;

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
    }
}
