using CAR.ApplicationServices.Services;
using CAR.Core.Dto;
using CAR.Data;
using Microsoft.EntityFrameworkCore;

namespace CAR.Tests
{
    public class CarUpdateTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task UpdateCar_ShouldChangeCarProperties()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            var createDto = new CarDto
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = 2020,
                Color = "Red",
                Price = 20000
            };

            var createdCar = await service.Create(createDto);
            var originalModifiedAt = createdCar.ModifiedAt;

            var updateDto = new CarDto
            {
                Id = createdCar.Id,
                Make = "Honda",
                Model = "Civic",
                Year = 2023,
                Color = "Blue",
                Price = 25000
            };

            // Act
            var updatedCar = await service.Update(updateDto);

            // Assert 
            Assert.NotNull(updatedCar);
            Assert.Equal(createdCar.Id, updatedCar.Id);
            Assert.Equal("Honda", updatedCar.Make);
            Assert.Equal("Civic", updatedCar.Model);
            Assert.Equal(2023, updatedCar.Year);
            Assert.Equal("Blue", updatedCar.Color);
            Assert.Equal(25000, updatedCar.Price);

            // ModifiedAt peaks olema muutunud
            Assert.NotEqual(originalModifiedAt, updatedCar.ModifiedAt);
        }

        [Fact]
        public async Task UpdateNonExistentCar_ShouldReturnNull()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            var updateDto = new CarDto
            {
                Id = 999,
                Make = "Test",
                Model = "Test",
                Year = 2024,
                Color = "Test",
                Price = 10000
            };

            // Act
            var result = await service.Update(updateDto);

            // Assert
            Assert.Null(result); 
        }
    }
}
