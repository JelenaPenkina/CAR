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
    }
}
