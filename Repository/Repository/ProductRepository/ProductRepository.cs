using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AplicationDbContext _context;

        public ProductRepository(AplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await this._context.Set<Product>()
                .ToListAsync();
        }
    }
}
