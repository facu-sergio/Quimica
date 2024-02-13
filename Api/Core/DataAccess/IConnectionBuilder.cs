using Microsoft.Data.SqlClient;

namespace Quimica.Core.DataAccess
{
    public interface IConnectionBuilder
    {
        SqlConnection GetConnection();
    }
}
