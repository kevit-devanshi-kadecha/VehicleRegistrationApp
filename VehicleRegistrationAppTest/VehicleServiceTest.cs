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
             var vehicle = _fixture.Build<VehicleModel>()
                      .Without(v => v.User) 
                      .Create();
            _vehicleService.Setup(v => v.AddVehicle(It.IsAny<VehicleModel>())).ReturnsAsync(vehicle);
            
            //Act
            var result = await _vehicleService.Object.AddVehicle(vehicle);
           
            //Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.VehicleId, result.VehicleId);
        }
        #endregion

        #region Edit Vehicle
         // Test for Editing a Vehicle with Null Data
         [Fact]
         public async Task EditVehicle_WithNullData()
         {
             // Arrange
             VehicleModel vehicle = null;
             var userId = _fixture.Create<string>();
        
             // Assert
             await Assert.ThrowsAsync<ArgumentNullException>(() => _vehicleServiceMock.EditVehicle(vehicle, userId));
         }
        
         // Test for Editing a Vehicle with Valid Data
         [Fact]
         public async Task EditVehicle_WithProperData()
         {
             // Arrange
             var vehicle = _fixture.Create<VehicleModel>();
             var userId = vehicle.UserId.ToString();
        
             _vehicleService.Setup(v => v.EditVehicle(It.IsAny<VehicleModel>(), userId)).ReturnsAsync(vehicle);
        
             // Act
             var result = await _vehicleServiceMock.EditVehicle(vehicle, userId);
        
             // Assert
             Assert.NotNull(result);
             Assert.Equal(vehicle.VehicleId, result.VehicleId);
         }
        
         // Test for Editing a Vehicle that does not exist
         [Fact]
         public async Task EditVehicle_VehicleNotFound()
         {
             // Arrange
             var vehicle = _fixture.Create<VehicleModel>();
             var userId = vehicle.UserId.ToString();
        
             _vehicleService.Setup(v => v.EditVehicle(It.IsAny<VehicleModel>(), userId))
                            .ThrowsAsync(new NullReferenceException("Vehicle not found."));
        
             // Assert
             await Assert.ThrowsAsync<NullReferenceException>(() => _vehicleServiceMock.EditVehicle(vehicle, userId));
         }
        
        #endregion
            
        #region Delete Vehicle

        // Test for Deleting a Vehicle that does not exist
        [Fact]
        public async Task DeleteVehicle_VehicleNotFound()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            _vehicleService.Setup(v => v.DeleteVehicle(vehicleId)).ReturnsAsync((VehicleModel)null);
        
            // Act
            var result = await _vehicleServiceMock.DeleteVehicle(vehicleId);
        
            // Assert
            Assert.Null(result);
        }
        
        // Test for Deleting a Vehicle with Valid Data
        [Fact]
        public async Task DeleteVehicle_WithProperData()
        {
            // Arrange
            var vehicle = _fixture.Create<VehicleModel>();
            _vehicleService.Setup(v => v.DeleteVehicle(vehicle.VehicleId)).ReturnsAsync(vehicle);
        
            // Act
            var result = await _vehicleServiceMock.DeleteVehicle(vehicle.VehicleId);
        
            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.VehicleId, result.VehicleId);
        }
        #endregion

        #region Get Vehicle By Id

        // Test for Getting Vehicle By Id with Valid Id
        [Fact]
        public async Task GetVehicleById_WithProperId()
        {
            // Arrange
            var vehicle = _fixture.Create<VehicleModel>();
            var vehicleId = vehicle.VehicleId;
        
            // Setting up the mock to return the vehicle when GetVehicleByIdAsync is called
            _vehicleService.Setup(v => v.GetVehicleByIdAsync(vehicleId)).ReturnsAsync(vehicle);
        
            // Act
            var result = await _vehicleServiceMock.GetVehicleByIdAsync(vehicleId);
        
            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicle.VehicleId, result.VehicleId);
        }
        
        // Test for Getting Vehicle By Id with Invalid Id
        [Fact]
        public async Task GetVehicleById_WithInvalidId()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
        
            _vehicleService.Setup(v => v.GetVehicleByIdAsync(vehicleId)).ReturnsAsync((VehicleModel)null);
        
            // Act
            var result = await _vehicleServiceMock.GetVehicleByIdAsync(vehicleId);
        
            // Assert
            Assert.Null(result);
        }
        
        #endregion
    }
}
