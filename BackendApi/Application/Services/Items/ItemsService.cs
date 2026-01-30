using Application.Dtos;
using Intfastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Items
{
    public class ItemsService(ISubskuRepository subskuRepository) : IItemsService
    {
        public async Task UpdateSubskuAsync(SubskuUpdateDto[] subskus)
        {
            var subskuDict = subskus.ToDictionary(x => x.Id);

            var outdated = await subskuRepository.GetAll(x => subskuDict.Keys.Contains(x.Id), true).ToListAsync();

            foreach (var outsubsku in outdated)
            {
                var current = subskuDict[outsubsku.Id];
                outsubsku.Price = current.Price;
                outsubsku.PlaningY1s.Units = current.Unit;
            }
            await subskuRepository.SaveAsync();
        }
    }
}
