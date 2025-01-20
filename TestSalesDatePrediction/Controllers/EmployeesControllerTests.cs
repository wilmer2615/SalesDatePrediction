using Logic.EmployeeLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using Moq;
using SalesDatePrediction.Controllers;

namespace TestSalesDatePrediction.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeLogic> _mockEmployeeLogic;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockEmployeeLogic = new Mock<IEmployeeLogic>();
            _controller = new EmployeesController(_mockEmployeeLogic.Object);
        }

        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { EmpId = 1, FullName = "Sara Davis" },
                new EmployeeModel { EmpId = 2, FullName = "Don Funk" }
            };

            _mockEmployeeLogic.Setup(service => service.GetEmployeesAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result); // Verifica que es un 200 OK
            var returnValue = Assert.IsType<List<EmployeeModel>>(actionResult.Value); // Verifica que el valor retornado es una lista de empleados
            Assert.Equal(2, returnValue.Count); // Verifica que haya 2 empleados
        }

        [Fact]
        public async Task GetEmployees_ReturnsNotFound_WhenNoEmployeesExist()
        {
            // Arrange
            _mockEmployeeLogic.Setup(service => service.GetEmployeesAsync())
                .ReturnsAsync((List<EmployeeModel>)null); // Retorna null

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result); // Verifica que es un 404 Not Found

            // Asegúrate de que la respuesta es del tipo esperado
            var returnValue = Assert.IsType<ErrorResponse>(actionResult.Value);
            Assert.Equal("No hay empleados registrados en la base de datos!", returnValue.Message);
        }








    }
}
