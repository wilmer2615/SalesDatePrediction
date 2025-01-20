using Entities;

namespace Repository.Repository.ProductRepository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
    }
}
