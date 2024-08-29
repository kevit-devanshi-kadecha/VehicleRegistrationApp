using AutoFixture;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Core.Services;
using VehicleRegistration.Infrastructure;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistration.WebAPI.Controllers;
using VehicleRegistration.WebAPI.Models;

namespace VehicleRegistrationAppTest
{
    public class VehicleServiceTest
    {
        private readonly Mock<ApplicationDbContext> _dbContext;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IVehicleService> _vehicleService;
        private readonly VehicleService _vehicleServiceMock;
        private readonly IFixture _fixture;
        public VehicleServiceTest()
        {
            _fixture = new Fixture();
            
            //Creating Mock For Db context 
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>( new DbContextOptionsBuilder<ApplicationDbContext>().Options);
            ApplicationDbContext dbContext = dbContextMock.Object;

            // inital data for DB by using mock 
            var usersInitialData = new List<UserModel>() { };
            var vehiclesInitialData = new List<VehicleModel>() { };
            
            //mocks for DbSet 
            dbContextMock.CreateDbSetMock(temp => temp.VehiclesDetails, vehiclesInitialData); 
            dbContextMock.CreateDbSetMock(temp => temp.Users, usersInitialData);

            //Create services based on mocked DbContext object
            _userService = new Mock<IUserService>(dbContext);
            _vehicleService = new Mock<IVehicleService>(dbContext);

            // fixture object for Creating vehicle data 
            var vehicle = _fixture.Create<VehicleModel>();
            _vehicleService.Setup(v => v.GetVehicleByIdAsync(It.IsAny<Guid>())).ReturnsAsync(vehicle);

            // has vehicleService depends on userService and applicationDbContext inject dependencies mock object
            _vehicleServiceMock = new VehicleService(dbContextMock.Object, _userService.Object);
        }

        #region Add Vehicle

        // Test for Adding a Vehicle with Null Data
        [Fact]
        public async Task AddVehicle_WithNullData()
        {
            //Arrange
            VehicleModel vehicle = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _vehicleService.Object.AddVehicle(vehicle));
        }

        // Test for Adding a Vehicle with Valid Data 
        [Fact]
        public async Task AddVehicle_WithProperData()
        {
            // Arrange
            var vehicle = _fixture.Create<VehicleModel>();
            _vehicleService.Setup(v => v.AddVehicle(It.IsAny<VehicleModel>())).ReturnsAsync(vehicle);
            
            //Act
            var result = await _vehicleService.Object.AddVehicle(vehicle);
           
            //Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.VehicleId, result.VehicleId);
        }
        #endregion


    }
}