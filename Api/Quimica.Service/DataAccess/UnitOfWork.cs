using Quimica.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly DapperDataContext _dapperDataContext;
        public IShipmentRepository Shipments { get; private set; }
        public IProductRepository Products { get; private set; }
        public IShipmentsProductsRepository ShipmentsProducts { get; private set; }

        public UnitOfWork(DapperDataContext dapperDataContext)
        {
            _dapperDataContext = dapperDataContext;
            Init();
        }

        // Método genérico que retorna una nueva instancia del repositorio
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dapperDataContext);
        }
        private void Init()
        {
            Shipments = new ShipmentRepository(_dapperDataContext);
            Products = new ProductRepository(_dapperDataContext);
            ShipmentsProducts = new ShipmentsProductRepository(_dapperDataContext);
        }


        public void BeginTransaction()
        {
            _dapperDataContext.Connection?.Open();
            _dapperDataContext.Transaction = _dapperDataContext.Connection?.BeginTransaction();
        }

        public void Commit()
        {
            _dapperDataContext.Transaction?.Commit();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
        }

        public void CommitAndCloseConnection()
        {
            _dapperDataContext.Transaction?.Commit();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
            _dapperDataContext.Connection?.Close();
            _dapperDataContext.Connection?.Dispose();
        }

        public void Rollback()
        {
            _dapperDataContext.Transaction?.Rollback();
            _dapperDataContext.Transaction?.Dispose();
            _dapperDataContext.Transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {

                if (disposing)
                {
                    _dapperDataContext.Transaction?.Dispose();
                    _dapperDataContext.Connection?.Dispose();
                }

                _disposed = true;
            }

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
 }
