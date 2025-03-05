using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.DataAccess
{
    public class ShipmentsProductRepository : GenericRepository<shipments_products>, IShipmentsProductsRepository
    {
        public ShipmentsProductRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
