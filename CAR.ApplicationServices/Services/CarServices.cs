using CAR.Core.Domain;
using CAR.Core.Dto;
using CAR.Core.Interface;
using CAR.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Car> Update(CarDto dto)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (car == null)
            {
                return null;
            }

            car.Make = dto.Make;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.Color = dto.Color;
            car.Price = dto.Price;
            car.ModifiedAt = DateTime.Now;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> GetAsync(int id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
        public async Task<Car> Delete(int id)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return null;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            var result = await _context.Cars.ToListAsync();
            return result;
        }
    }
}
