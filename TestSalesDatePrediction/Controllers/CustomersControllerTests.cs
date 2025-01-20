using Logic.CustomerLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using Moq;
using SalesDatePrediction.Controllers;

namespace TestSalesDatePrediction.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerLogic> _mockCustomerLogic;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockCustomerLogic = new Mock<ICustomerLogic>();
            _controller = new CustomersController(_mockCustomerLogic.Object);
        }

        [Fact]
        public async Task SalesDatePrediction_ReturnsOkWithResults_WhenDataExists()
        {
            // Arrange
            string customerName = "Test Customer";
            var mockData = new List<DatePredictionModel>
            {
                new DatePredictionModel { LastOrderDate = new DateTime(2024, 1, 20) },
                new DatePredictionModel { LastOrderDate = new DateTime(2024, 2, 15) }
            }; // Simulación de datos devueltos

            _mockCustomerLogic
                .Setup(x => x.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _controller.SalesDatePrediction(customerName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(mockData, okResult.Value);
        }


        [Fact]
        public async Task SalesDatePrediction_ReturnsOkWithEmptyList_WhenNoDataExists()
        {
            // Arrange
            string customerName = "NonExistingCustomer";
            var mockData = new List<DatePredictionModel>(); // Simulación de datos vacíos

            _mockCustomerLogic
                .Setup(x => x.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _controller.SalesDatePrediction(customerName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Empty(okResult.Value as IEnumerable<DatePredictionModel>);
        }

        
        [Fact]
        public async Task SalesDatePrediction_ReturnsOkWithAllData_WhenNoFilterProvided()
        {
            // Arrange
            string? customerName = null; // Sin filtro
            var mockData = new List<DatePredictionModel>
            {
                new DatePredictionModel { LastOrderDate = new DateTime(2024, 1, 20) },
                new DatePredictionModel { LastOrderDate = new DateTime(2024, 2, 15) },
                new DatePredictionModel { LastOrderDate = new DateTime(2024, 3, 10) }
            };

            _mockCustomerLogic
                .Setup(x => x.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _controller.SalesDatePrediction(customerName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(mockData, okResult.Value);
        }
    }
}
