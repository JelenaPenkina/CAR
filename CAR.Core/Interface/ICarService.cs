using CAR.Core.Domain;
using CAR.Core.Dto;

namespace CAR.Core.Interface
{
    public interface ICarService
    {
        Task<Car> Create(CarDto dto);
        Task<Car> Update(CarDto dto);
        Task<Car> GetAsync(int id);

    }
}
