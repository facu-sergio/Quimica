using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, IDbConnection connection = null);
        Task<IEnumerable<T>> GetAllAsync(IDbConnection connection = null);
        Task<int> AddAsync(T entity, IDbTransaction transaction = null, IDbConnection connection = null);
        Task UpdateAsync(T entity, string keyProperty = "Id", IDbTransaction transaction = null, IDbConnection connection = null);
        Task DeleteByIdAsync(int id, IDbConnection connection = null, IDbTransaction transaction = null);
        Task<int> DeleteAsync(string condition, object parameters, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
