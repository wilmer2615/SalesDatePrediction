using Logic.CustomerLogic;
using Models.Response;
using Moq;
using Repository.Repository.CustomerRepository;

namespace TestLogic.CustomerLogic
{
    public class CustomerLogicTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly ICustomerLogic _customerLogic;

        public CustomerLogicTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _customerLogic = new Logic.CustomerLogic.CustomerLogic(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task SalesDatePredictionAsync_ReturnsResults_WhenDataExists()
        {
            // Arrange
            string? customerName = "ExistingCustomer";
            var mockData = new List<DatePredictionModel>
            {
                new DatePredictionModel { CustId = 1, CustomerName = "Customer NRZBB", LastOrderDate = new DateTime(2023, 02, 20) },
                new DatePredictionModel { CustId = 2, CustomerName = "Customer MLTDN", LastOrderDate = new DateTime(2023, 04, 1) }
            };

            _mockCustomerRepository
                .Setup(repo => repo.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _customerLogic.SalesDatePredictionAsync(customerName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(mockData, result);
        }

        [Fact]
        public async Task SalesDatePredictionAsync_ReturnsEmptyList_WhenNoDataExists()
        {
            // Arrange
            string? customerName = "NonExistingCustomer";
            var mockData = new List<DatePredictionModel>(); // Empty list

            _mockCustomerRepository
                .Setup(repo => repo.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _customerLogic.SalesDatePredictionAsync(customerName);

            // Assert
            Assert.NotNull(result); // Verifica que la lista no sea nula
            Assert.Empty(result); // Verifica que la lista esté vacía
        }

        [Fact]
        public async Task SalesDatePredictionAsync_HandlesNullCustomerName()
        {
            // Arrange
            string? customerName = null;
            var mockData = new List<DatePredictionModel>
            {
                new DatePredictionModel { CustId = 1, CustomerName = "Customer XYZ", LastOrderDate = new DateTime(2023, 01, 01) }
            };

            _mockCustomerRepository
                .Setup(repo => repo.SalesDatePredictionAsync(customerName))
                .ReturnsAsync(mockData);

            // Act
            var result = await _customerLogic.SalesDatePredictionAsync(customerName);

            // Assert
            Assert.NotNull(result); // Verifica que no sea nulo
            Assert.Single(result); // Verifica que haya un solo elemento en la lista
            Assert.Equal(mockData, result); // Compara el resultado con los datos simulados
        }
    }
}