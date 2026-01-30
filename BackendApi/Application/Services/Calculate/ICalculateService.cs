using Application.Dtos;

namespace Application.Services.Calculate
{
    public interface ICalculateService
    {
        Task<CalculationDto> Calculate(int[] selectedSubSkus);
    }
}
