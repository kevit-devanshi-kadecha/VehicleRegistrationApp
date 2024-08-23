using AutoFixture;
using Moq;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Infrastructure;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.WebAPI.Controllers;

namespace VehicleRegistrationAppTest
{
    public class VehicleControllerTest
    {
        private readonly IVehicleService _vehicleService;
        private readonly Mock<IVehicleService> _vehicleServiceMock;
        private readonly Mock<ApplicationDbContext> _dbContext;
        private readonly Fixture _fixture;
        public VehicleControllerTest()
        {
            _fixture = new Fixture();
            _vehicleServiceMock = new Mock<IVehicleService>();
            // calling mocked object 
            _vehicleService = _vehicleServiceMock.Object;
            _dbContext = new Mock<ApplicationDbContext>();
        }

        #region GetVehicles

        [Fact]
        public void GetVehiclesList()
        {
            //Arrange
            //Act
            List<VehicleModel> vehicles_list = _fixture.Create<List<VehicleModel>>();
            //Assert
            Assert.Empty(vehicles_list);
        }

        [Fact]
        public void GetVehicles_WithEmptyList()
        {

        }
        #endregion


        #region Add Vehicle
        [Fact]
        public void AddVehicle_WithInitalData()
        {
            // Arrange
            
            //Act
            var vehicle = _fixture.Build<VehicleModel>().With(v => v.VehicleNumber, "GJ 34 1236").Create();

            //Assert
        }
        #endregion
    }
}