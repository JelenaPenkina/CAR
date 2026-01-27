using CAR.Core.Domain;
using CAR.Core.Dto;
using CAR.Core.Interface;
using CAR.Data;

namespace CAR.ApplicationServices.Services
{
    public class CarServices : ICarService
    {
        private readonly AppDbContext _context;

        public CarServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Car> Create(CarDto dto)
        {
            var car = new Car
            {
                Id = dto.Id,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                Price = dto.Price,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }
    }
}
