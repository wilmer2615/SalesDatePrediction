using Logic.ProductLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using Moq;
using SalesDatePrediction.Controllers;

namespace TestSalesDatePrediction.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductLogic> _mockProductLogic;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductLogic = new Mock<IProductLogic>();
            _controller = new ProductsController(_mockProductLogic.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WhenProductsExist()
        {
            // Arrange
            var employees = new List<ProductModel>
            {
                new ProductModel { ProductId = 1, ProductName = "Prueba1" },
                new ProductModel { ProductId = 2, ProductName = "Prueba2" }
            };

            _mockProductLogic.Setup(service => service.GetProductsAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result); // Verifica que es un 200 OK
            var returnValue = Assert.IsType<List<ProductModel>>(actionResult.Value); // Verifica que el valor retornado es una lista de productos
            Assert.Equal(2, returnValue.Count); // Verifica que haya 2 productos
        }

        [Fact]
        public async Task GetProducts_ReturnsNotFound_WhenNoProductsExist()
        {
            // Arrange
            _mockProductLogic.Setup(service => service.GetProductsAsync())
                .ReturnsAsync((List<ProductModel>)null); // Retorna null

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result); // Verifica que es un 404 Not Found

            // Asegúrate de que la respuesta es del tipo esperado
            var returnValue = Assert.IsType<ErrorResponse>(actionResult.Value);
            Assert.Equal("No hay productos registrados en la base de datos!", returnValue.Message);
        }








    }
}
