using Application.Dtos;
using Application.Models;
using AutoMapper;
using Intfastructure.Repositories;
using Models;

namespace Application.Services.Calculate
{
    public class CalculateService(ISkuRepository skuRepository, IMapper mapper) : ICalculateService
    {
        public async Task<CalculationDto> Calculate(int[] selectedSubSkus)
        {
            var skus = await skuRepository.GetAllAsync(selectedSubSkus);

            var calculationResult = new CalculationModel();
            foreach (var sku in skus)
            {   
                var tempSku = new SkuModel(sku.Id, sku.Name);
                foreach (var sub in sku.SubSkus)
                {
                    tempSku.AddSubSku(GetSubSkuData(sub));
                }
                tempSku.CalculatePriceH0();
                tempSku.CalculatePriceY1();
                tempSku.CalculateContributionGrowth();

                calculationResult.AddSku(tempSku);
            }
            calculationResult.CalculateTotalPrice();
            calculationResult.CalculateTotalContributionGrowth();
            return mapper.Map<CalculationDto>(calculationResult);
        }

        private static SubSkuModel GetSubSkuData(SubSku sub)
        {
            var history = sub.HistoryY0s;
            var plan = sub.PlaningY1s;

            var block = new SubSkuModel(sub.Price, sub.Ratio, history.Units, history.Amount, plan.Units, plan.Amount)
            {
                Id = sub.Id,
                Name = sub.Name
            };
            return block;
        }
    }
}
