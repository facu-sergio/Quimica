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
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
       

        public  async Task<List<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
