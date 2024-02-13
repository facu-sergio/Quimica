using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System.Data;

namespace Quimica.Service.DataAccess
{
    public class ProductRepository : IproductRepository
    {
        private IConnectionBuilder _connectionBuilder;
        private ILogger<ProductRepository> _logger;
        public ProductRepository(IConnectionBuilder connectionBuilder ,ILogger<ProductRepository> logger)
        {
            _connectionBuilder = connectionBuilder;
            _logger = logger;
        }
        

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"SELECT * FROM Products ";

                    List<Product> products = (await db.QueryAsync<Product>(query)).ToList();

                return products;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProductRepository/GetProductsAsyc)");
                throw ex;
            }
        }

        public async Task InsertProduct(Product product)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"INSERT INTO Products (name) VALUES (@productName)";

                    await db.ExecuteAsync(query, new { productName = product.Name });
                }
            } catch(Exception ex)
            {
                _logger.LogError(ex, $"ProductRepository/InsertProduct({JsonConvert.SerializeObject(product)})");
                throw ex;
            }
        }


        public async Task DeleteProduct(int productId)
        {
            try
            {
                using (IDbConnection db = _connectionBuilder.GetConnection())
                {
                    string query = @"DELETE FROM Products WHERE id=@productId";

                    await db.ExecuteAsync(query, new { productId = productId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ProductRepository/DeleteProduct(id:{productId})");
                throw ex;
            }
        }
    }
}
