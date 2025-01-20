using AutoMapper;
using Entities;
using Logic.EmployeeLogic;
using Logic.ProductLogic;
using Models.Response;
using Moq;
using Repository.Repository.EmployeeRepository;
using Repository.Repository.ProductRepository;

namespace TestLogic.EmployeeLogic
{
    public class EmployeeLogicTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly IEmployeeLogic _employeeLogic;
        private readonly Mock<IMapper> _mockMapper;

        public EmployeeLogicTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _employeeLogic = new Logic.EmployeeLogic.EmployeeLogic(_mockMapper.Object, _mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsMappedEmployees_WhenRepositoryReturnsData()
        {
            // Arrange
            var employeesFromRepo = new List<Employee>
            {
                new Employee { Empid = 1, FirstName = "John" },
                new Employee { Empid = 2, FirstName = "Jane" }
            };

            var mappedEmployees = new List<EmployeeModel>
            {
                new EmployeeModel { EmpId = 1, FullName = "John Doe" },
                new EmployeeModel { EmpId = 2, FullName = "Jane Smith" }
            };

            // Configurar el mock del repositorio para retornar datos
            _mockEmployeeRepository
                .Setup(repo => repo.GetEmployeesAsync())
                .ReturnsAsync(employeesFromRepo);

            // Configurar el mock del mapper para mapear los datos
            _mockMapper
                .Setup(mapper => mapper.Map<List<EmployeeModel>>(employeesFromRepo))
                .Returns(mappedEmployees);

            // Act
            var result = await _employeeLogic.GetEmployeesAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Equal(2, result.Count()); // Asegurarse de que el conteo de empleados es correcto
            Assert.Equal("John Doe", result.First().FullName); // Validar que el primer empleado tiene el nombre correcto

            // Verificar que los mocks fueron llamados correctamente
            _mockEmployeeRepository.Verify(repo => repo.GetEmployeesAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<EmployeeModel>>(employeesFromRepo), Times.Once);
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            var employeesFromRepo = new List<Employee>(); // Repositorio retorna una lista vacía
            var mappedEmployees = new List<EmployeeModel>(); // Mapper debe retornar también una lista vacía

            // Configurar el mock del repositorio
            _mockEmployeeRepository
                .Setup(repo => repo.GetEmployeesAsync())
                .ReturnsAsync(employeesFromRepo);

            // Configurar el mock del mapper
            _mockMapper
                .Setup(mapper => mapper.Map<List<EmployeeModel>>(employeesFromRepo))
                .Returns(mappedEmployees);

            // Act
            var result = await _employeeLogic.GetEmployeesAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Empty(result); // Asegurarse de que el resultado está vacío

            // Verificar que los mocks fueron llamados correctamente
            _mockEmployeeRepository.Verify(repo => repo.GetEmployeesAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<EmployeeModel>>(employeesFromRepo), Times.Once);
        }

    }
}