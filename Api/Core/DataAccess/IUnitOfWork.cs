using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        public IShipmentRepository Shipments { get; }
        public IProductRepository Products { get; }
        public IShipmentsProductsRepository ShipmentsProducts { get; }
        void BeginTransaction();
        void Commit();
        void CommitAndCloseConnection();
        void Rollback();
    }
}
