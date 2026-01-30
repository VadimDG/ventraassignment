using Application.Dtos;

namespace Application.Services.Items
{
    public interface IItemsService
    {
        Task UpdateSubskuAsync(SubskuUpdateDto[] subskus);
    }
}
