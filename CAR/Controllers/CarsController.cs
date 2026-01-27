using CAR.Core.Interface;
using CAR.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAR.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICarService _carService;

        public CarsController(AppDbContext context, ICarService carService)
        {
            _context = context;
            _carService = carService;
        }
        // GET: /Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync();
            return View(cars);
        }

    }
}
