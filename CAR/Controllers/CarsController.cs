using CAR.Core.Dto;
using CAR.Core.Interface;
using CAR.Data;
using CAR.Models;
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

        // GET: /Cars/Create
        public IActionResult Create()
        {
            var vm = new CarViewModel();
            return View(vm);
        }

        // POST:/Cars/Create
        [HttpPost]
        public async Task<IActionResult> Create(CarViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new CarDto
            {
                Id = vm.Id,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                Color = vm.Color,
                Price = vm.Price,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var result = await _carService.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Car created successfully!";
            return RedirectToAction(nameof(Index));
        }
        // GET: /Cars/Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var car = await _carService.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        // POST: /Cars/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CarViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var dto = new CarDto
            {
                Id = vm.Id,
                Make = vm.Make,
                Model = vm.Model,
                Year = vm.Year,
                Color = vm.Color,
                Price = vm.Price,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now
            };

            var result = await _carService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Car '{result.Make} {result.Model}' updated successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
