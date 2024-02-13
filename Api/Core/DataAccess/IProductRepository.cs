using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Core.DataAccess
{
    public interface IproductRepository
    {
        Task<List<Product>> GetProductsAsync();

        Task InsertProduct(Product product);

        Task DeleteProduct(int productId);

    }
}
