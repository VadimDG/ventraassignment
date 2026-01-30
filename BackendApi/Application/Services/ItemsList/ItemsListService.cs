using Application.Dtos;
using Intfastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ItemsList
{
    public class ItemsListService(ISubskuRepository subskuRepository) : IItemsListService
    {
        public Task<List<IdNameDto>> GetSubskuNames() => subskuRepository.GetAll().Select(s => new IdNameDto { Id = s.Id, Name = s.Name }).ToListAsync();
    }
}
