using AutoMapper;
using Models.Response;
using Repository.Repository.ProductRepository;

namespace Logic.ProductLogic
{
    public class ProductLogic : IProductLogic
    {
        private readonly IMapper _mapper;

        private readonly IProductRepository _productRepository;

        public ProductLogic(IMapper mapper, IProductRepository productRepository)
        {
            this._mapper = mapper;
            this._productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var entities = await this._productRepository.GetProductsAsync();

            var result = _mapper.Map<List<ProductModel>>(entities);

            return result;
        }
    }
}
