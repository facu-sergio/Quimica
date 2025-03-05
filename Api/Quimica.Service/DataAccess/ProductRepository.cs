using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System.Data;

namespace Quimica.Service.DataAccess
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
        {
        }
    }
}
