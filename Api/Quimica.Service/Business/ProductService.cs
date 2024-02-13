using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quimica.Service.Business
{
    public class ProductService : IProductService
    {
        private readonly IproductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IproductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
       

        public  async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                return await _productRepository.GetProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProductService/GetProductsAsync");
                throw ex;
            }
        }

        public async Task InsertProduct(Product product)
        {
            try
            {
                await _productRepository.InsertProduct(product);
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, $"ProductService/InsertOrUpdate({JsonConvert.SerializeObject(product)})");
                throw ex;
            }
        }

        public async Task DeleteProduct(int productId)
        {
            try
            {
                await _productRepository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ProductService/DeleteProduct(ProductId:{productId})");
                throw ex;
            }
        }
    }
}
