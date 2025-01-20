using Models.Response;

namespace Logic.ProductLogic
{
    public interface IProductLogic
    {
        public Task<IEnumerable<ProductModel>> GetProductsAsync();
    }
}
