using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EnterpriceContext context;

        public ProductRepository(EnterpriceContext context)
        {
            this.context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);

            await context.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(int id)
        {
            var deletedProduct = await context.Products.Where(x => x.Id == id).Select(x => x).FirstOrDefaultAsync();
            context.Products.Remove(deletedProduct);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserIdAsync(int id) =>
            await context.Products.Where(x => x.UserId == id).Select(x => x).AsNoTracking().ToListAsync();

        public async Task<bool> IsProductExistsByIdAsync(int id)
        {
            var product = await context.Products.Where(x => x.Id == id).Select(x => x).AsNoTracking().FirstOrDefaultAsync();

            return product == null ? false : true;
        }

        public async Task UpdateProductAsync(Product product)
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
