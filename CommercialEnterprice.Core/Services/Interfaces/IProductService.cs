using CommercialEnterprise.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialEnterprise.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllByUserIdAsync(int id);
        Task AddProductAsync(Product product);
        Task DeleteProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<bool> IsProductExistsById(int id);
    }
}
