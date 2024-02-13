using Quimica.Core.Models;

namespace Quimica.Core.Bussiness
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task InsertProduct(Product product);
        Task DeleteProduct(int productId);
    }
}
