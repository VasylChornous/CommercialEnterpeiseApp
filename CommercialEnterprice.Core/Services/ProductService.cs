using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task AddProductAsync(Product product) =>
            await productRepository.AddProductAsync(product);

        public async Task DeleteProductByIdAsync(int id) =>
            await productRepository.DeleteProductByIdAsync(id);

        public async Task<IEnumerable<Product>> GetAllByUserIdAsync(int id) =>
            await productRepository.GetAllByUserIdAsync(id);

        public async Task UpdateProductAsync(Product product) =>
            await productRepository.UpdateProductAsync(product);

        public async Task<bool> IsProductExistsById(int id) =>
            await productRepository.IsProductExistsByIdAsync(id);
    }
}
