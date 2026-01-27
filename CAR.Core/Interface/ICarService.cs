using CAR.Core.Domain;
using CAR.Core.Dto;

namespace CAR.Core.Interface
{
    public interface ICarService
    {
        Task<Car> Create(CarDto dto);

    }
}
