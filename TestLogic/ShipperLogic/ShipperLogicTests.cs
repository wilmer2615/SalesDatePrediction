using AutoMapper;
using Entities;
using Logic.ShipperLogic;
using Models.Response;
using Moq;
using Repository.Repository.ShipperRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLogic.ShipperLogic
{
    public class ShipperLogicTests
    {
        private readonly Mock<IShipperRepository> _mockShipperRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IShipperLogic _shipperLogic;

        public ShipperLogicTests()
        {
            _mockShipperRepository = new Mock<IShipperRepository>();
            _mockMapper = new Mock<IMapper>();
            _shipperLogic = new Logic.ShipperLogic.ShipperLogic(_mockMapper.Object, _mockShipperRepository.Object);
        }

        [Fact]
        public async Task GetShippersAsync_ReturnsMappedShippers_WhenRepositoryReturnsData()
        {
            // Arrange
            var shippersFromRepo = new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "DHL" },
                new Shipper { ShipperId = 2, CompanyName = "FedEx" }
            };

            var mappedShippers = new List<ShipperModel>
            {
                new ShipperModel { ShipperId = 1, CompanyName = "DHL" },
                new ShipperModel { ShipperId = 2, CompanyName = "FedEx" }
            };

            // Configurar el mock del repositorio para retornar datos
            _mockShipperRepository
                .Setup(repo => repo.GetShippersAsync())
                .ReturnsAsync(shippersFromRepo);

            // Configurar el mock del mapper para mapear los datos
            _mockMapper
                .Setup(mapper => mapper.Map<List<ShipperModel>>(shippersFromRepo))
                .Returns(mappedShippers);

            // Act
            var result = await _shipperLogic.GetShippersAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Equal(2, result.Count()); // Asegurarse de que el conteo de shippers es correcto
            Assert.Equal("DHL", result.First().CompanyName); // Validar que el primer shipper tiene el nombre correcto

            // Verificar que los mocks fueron llamados correctamente
            _mockShipperRepository.Verify(repo => repo.GetShippersAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ShipperModel>>(shippersFromRepo), Times.Once);
        }

        [Fact]
        public async Task GetShippersAsync_ReturnsEmptyList_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            var shippersFromRepo = new List<Shipper>(); // Repositorio retorna una lista vacía
            var mappedShippers = new List<ShipperModel>(); // Mapper debe retornar también una lista vacía

            // Configurar el mock del repositorio
            _mockShipperRepository
                .Setup(repo => repo.GetShippersAsync())
                .ReturnsAsync(shippersFromRepo);

            // Configurar el mock del mapper
            _mockMapper
                .Setup(mapper => mapper.Map<List<ShipperModel>>(shippersFromRepo))
                .Returns(mappedShippers);

            // Act
            var result = await _shipperLogic.GetShippersAsync();

            // Assert
            Assert.NotNull(result); // Asegurarse de que el resultado no es nulo
            Assert.Empty(result); // Asegurarse de que el resultado está vacío

            // Verificar que los mocks fueron llamados correctamente
            _mockShipperRepository.Verify(repo => repo.GetShippersAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<ShipperModel>>(shippersFromRepo), Times.Once);
        }
    }
}
