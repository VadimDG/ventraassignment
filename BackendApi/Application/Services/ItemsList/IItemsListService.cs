using Application.Dtos;

namespace Application.Services.ItemsList
{
    public interface IItemsListService
    {
        Task<List<IdNameDto>> GetSubskuNames();
    }
}
