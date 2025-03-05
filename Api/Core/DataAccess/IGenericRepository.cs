using Quimica.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters, params string[] selectData);
        Task<T> GetByIdAsync(int id, params string[] selectData);
        Task<IEnumerable<T>> GetBySpecificColumnAsync(string columnName, object columnValue, params string[] selectData);
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SoftDeleteAsync(int id, bool softDeleteFromRelatedChildTables = false);
        Task<IEnumerable<T>> GetByFilterAsync(Dictionary<string, object> filters, params string[] selectData);
        Task<int> GetTotalCountAsync();
        Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue);
    }
}
