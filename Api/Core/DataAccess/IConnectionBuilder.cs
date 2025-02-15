using Microsoft.Data.SqlClient;

namespace Quimica.Core.DataAccess
{
    public interface IConnectionBuilder
    {
        SqlConnection GetConnection();

        string GetConnectionString();

        string GetTableName<T>();
    }
}
