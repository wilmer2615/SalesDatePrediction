using AutoMapper;
using Entities;
using Logic.ProductLogic;
using Models.Response;
using Moq;
using Repository.Repository.ProductRepository;

namespace TestLogic.ProductLogic
{
    public class ProductLogicTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly IProductLogic _productLogic;
        private readonly Mock<IMapper> _mockMapper;

        public ProductLogicTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _productLogic = new Logic.ProductLogic.ProductLogic(_mockMapper.Object, _mockProductRepository.Object);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsMappedProducts_WhenRepositoryReturnsData()
        {
            // Arrange
            var productsFromRepo = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product 1" },
                new Product { ProductId = 2, ProductName = "Product 2" }
            };

            var mappedProducts = new List<ProductModel>
            {
                new ProductModel { ProductId = 1, ProductName = "Product 1" },
                new ProductModel { ProductId = 2, ProductName = "Product 2" }
            };

            // Configurar el mock del repositorio para retornar datos
            _mockProductRepository
                .Setup(repo => repo.GetProductsAsync())
                .ReturnsAsync(productsFromRepo);

            // Configurar el mock del mapper para mapear los datos
            _mockMapper
                .Setup(mapper => mapper.Map<List<ProductModel>>(productsFromRepo))
                .Returns(mappedProducts);

            // Act
            var result = await _productLogic.GetProductsAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Equal(2, result.Count()); // Asegurarse de que el conteo de productos es correcto
            Assert.Equal("Product 1", result.First().ProductName); // Validar que el primer producto tiene el nombre correcto

            // Verificar que los mocks fueron llamados correctamente
            _mockProductRepository.Verify(repo => repo.GetProductsAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ProductModel>>(productsFromRepo), Times.Once);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            var productsFromRepo = new List<Product>(); // Repositorio retorna una lista vacía
            var mappedProducts = new List<ProductModel>(); // Mapper debe retornar también una lista vacía

            // Configurar el mock del repositorio
            _mockProductRepository
                .Setup(repo => repo.GetProductsAsync())
                .ReturnsAsync(productsFromRepo);

            // Configurar el mock del mapper
            _mockMapper
                .Setup(mapper => mapper.Map<List<ProductModel>>(productsFromRepo))
                .Returns(mappedProducts);

            // Act
            var result = await _productLogic.GetProductsAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Empty(result); // Asegurarse de que el resultado está vacío

            // Verificar que los mocks fueron llamados correctamente
            _mockProductRepository.Verify(repo => repo.GetProductsAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ProductModel>>(productsFromRepo), Times.Once);
        }

    }
}