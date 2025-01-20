using Logic.ShipperLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using Moq;
using SalesDatePrediction.Controllers;

namespace TestSalesDatePrediction.Controllers
{
    public class ShippersControllerTests
    {
        private readonly Mock<IShipperLogic> _mockShipperLogic;
        private readonly ShippersController _controller;

        public ShippersControllerTests()
        {
            _mockShipperLogic = new Mock<IShipperLogic>();
            _controller = new ShippersController(_mockShipperLogic.Object);
        }

        [Fact]
        public async Task GetShippers_ReturnsOkResult_WhenShippersExist()
        {
            // Arrange
            var employees = new List<ShipperModel>
            {
                new ShipperModel { ShipperId = 1, CompanyName = "Prueba1" },
                new ShipperModel { ShipperId = 2, CompanyName = "Prueba2" }
            };

            _mockShipperLogic.Setup(service => service.GetShippersAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetShippers();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result); // Verifica que es un 200 OK
            var returnValue = Assert.IsType<List<ShipperModel>>(actionResult.Value); // Verifica que el valor retornado es una lista de transportistas
            Assert.Equal(2, returnValue.Count); // Verifica que haya 2 transportistas
        }

        [Fact]
        public async Task GetShippers_ReturnsNotFound_WhenNoShippersExist()
        {
            // Arrange
            _mockShipperLogic.Setup(service => service.GetShippersAsync())
                .ReturnsAsync((List<ShipperModel>)null); // Retorna null

            // Act
            var result = await _controller.GetShippers();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result); // Verifica que es un 404 Not Found

            // Asegúrate de que la respuesta es del tipo esperado
            var returnValue = Assert.IsType<ErrorResponse>(actionResult.Value);
            Assert.Equal("No hay transportistas registrados en la base de datos!", returnValue.Message);
        }








    }
}
