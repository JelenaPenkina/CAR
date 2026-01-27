using CAR.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace CAR.Tests
{
    public class CarYearValidationTests
    {
        [Theory]
        [InlineData(1885, false)]
        [InlineData(1886, true)]
        [InlineData(1900, true)]
        [InlineData(1950, true)]
        [InlineData(2024, true)]
        [InlineData(2026, true)]
        [InlineData(2027, false)]
        [InlineData(2100, false)]
        public void CarYear_WithVariousYears_ShouldValidateCorrectly(int year, bool expectedIsValid)
        {
            // Arrange
            var car = new Car
            {
                Make = "Test",
                Model = "Car",
                Year = year,
                Color = "Black",
                Price = 30000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var validationContext = new ValidationContext(car);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(car, validationContext, validationResults, true);

            // Assert
            Assert.Equal(expectedIsValid, isValid);

            if (!expectedIsValid)
            {
                Assert.Contains(validationResults,
                    v => v.ErrorMessage.Contains("Year must be between 1886"));
            }
        }

        [Fact]
        public void Car_WithYear1886_ShouldBeValid_FirstCarEver()
        {
            // Arrange
            var car = new Car
            {
                Make = "Benz",
                Model = "Patent-Motorwagen",
                Year = 1886,
                Color = "Black",
                Price = 1000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var validationContext = new ValidationContext(car);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(car, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Car_WithYear2027_ShouldBeInvalid_FutureYear()
        {
            // Arrange
            var car = new Car
            {
                Make = "Future",
                Model = "Car",
                Year = 2027,
                Color = "Silver",
                Price = 50000,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var validationContext = new ValidationContext(car);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(car, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults,
                v => v.ErrorMessage.Contains("2026"));
        }

        [Fact]
        public void Car_WithYear0_ShouldBeInvalid_BeforeCarsExisted()
        {
            // Arrange
            var car = new Car
            {
                Make = "Ancient",
                Model = "Chariot",
                Year = 0,
                Color = "Wood",
                Price = 10,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var validationContext = new ValidationContext(car);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(car, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }
    }
}
