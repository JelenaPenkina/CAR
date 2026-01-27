using CAR.ApplicationServices.Services;
using CAR.Core.Domain;
using CAR.Data;
using Microsoft.EntityFrameworkCore;

namespace CAR.Tests
{
    public class DeleteTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task DeleteFlow_WhenCancelled_ShouldNotDeleteCar()
        {

            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            var car = new Car
            {
                Make = "Toyota",
                Model = "Camry",
                Year = 2023,
                Color = "Blue",
                Price = 28000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var initialCount = await context.Cars.CountAsync();
            var carId = car.Id;

            Console.WriteLine($"Initial car count: {initialCount}");
            Console.WriteLine($"Car ID to delete: {carId}");

            // Act 
            // 1. Kasutaja kasutab Delete lehte -> see ei kustuta
            // 2. Kasutaja vajutab Cancel -> vaid läheb INDEX vaatese kasutajaga tagasi

            // Assert 
            var finalCount = await context.Cars.CountAsync();
            var carStillExists = await context.Cars.AnyAsync(c => c.Id == carId);

            Console.WriteLine($"Final car count: {finalCount}");
            Console.WriteLine($"Car still exists: {carStillExists}");

            Assert.Equal(initialCount, finalCount);
            Assert.True(carStillExists);
            Assert.Equal("Toyota", car.Make);
        }

        [Fact]
        public async Task DeleteFlow_WhenConfirmed_ShouldDeleteCar()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new CarServices(context);

            var car = new Car
            {
                Make = "ToDelete",
                Model = "Car",
                Year = 2023,
                Color = "Red",
                Price = 25000
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var initialCount = await context.Cars.CountAsync();
            var carId = car.Id;

            // Act
            var deletedCar = await service.Delete(carId);

            // Assert 
            var finalCount = await context.Cars.CountAsync();
            var carStillExists = await context.Cars.AnyAsync(c => c.Id == carId);

            Assert.NotNull(deletedCar);
            Assert.Equal("ToDelete", deletedCar.Make);
            Assert.Equal(initialCount - 1, finalCount);
            Assert.False(carStillExists);
        }
    }
}
