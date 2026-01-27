using CAR.ApplicationServices.Services;
using CAR.Core.Domain;
using CAR.Core.Dto;
using CAR.Data;
using Microsoft.EntityFrameworkCore;

namespace CAR.Tests
{
    public class CarServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateCar_WithValidData_ShouldCreateCar()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            var dto = new CarDto
            {
                Make = "BMW",
                Model = "X5",
                Year = 2024,
                Color = "Black",
                Price = 65000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // Act
            var result = await service.Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("BMW", result.Make);
            Assert.Equal("X5", result.Model);
            Assert.Equal(2024, result.Year);
            Assert.Equal("Black", result.Color);
            Assert.Equal(65000, result.Price);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task GetAllCars_ShouldReturnAllCars()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            context.Cars.AddRange(
                new Car { Make = "Audi", Model = "A4", Year = 2023, Color = "White", Price = 42000 },
                new Car { Make = "Mercedes", Model = "C-Class", Year = 2024, Color = "Silver", Price = 52000 }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
