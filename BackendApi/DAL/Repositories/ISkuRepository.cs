using Models;

namespace Intfastructure.Repositories
{
    public interface ISkuRepository
    {
        Task<List<Sku>> GetAllAsync(int[] selectedSubSkus);
        Task<Sku?> GetByIdAsync(int id);
    }
}