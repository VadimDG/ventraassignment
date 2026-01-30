using Models;
using Microsoft.EntityFrameworkCore;

namespace Intfastructure.Repositories
{
    public class SkuRepository(BackendDbContext context) : ISkuRepository
    {
        private readonly BackendDbContext _context = context;

        public async Task<List<Sku>> GetAllAsync(int[] selectedSubSkus)
        {
            var skus = await _context.Skus.AsNoTracking().ToListAsync();

            foreach (var sku in skus)
            {
                var temp = _context.SubSkus.AsNoTracking().Where(ss => ss.SkuId == sku.Id);

                if (selectedSubSkus.Length != 0)
                {
                    temp = temp.Where(x => selectedSubSkus.Contains(x.Id));
                }

                var subs = await temp.ToListAsync();

                foreach (var sub in subs)
                {
                    sub.HistoryY0s = await _context.HistoryY0s.AsNoTracking()
                        .FirstOrDefaultAsync(h => h.SubSkuId == sub.Id) ?? new HistoryY0 { SubSkuId = sub.Id, Units = 0, Amount = 0m };

                    sub.PlaningY1s = await _context.PlaningY1s.AsNoTracking()
                        .FirstOrDefaultAsync(p => p.SubSkuId == sub.Id) ?? new PlaningY1 { SubSkuId = sub.Id, Units = 0, Amount = 0m };
                }

                sku.SubSkus = [.. subs];
            }

            return skus;
        }

        public async Task<Sku?> GetByIdAsync(int id)
        {
            var sku = await _context.Skus.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (sku == null) return null;

            var subs = await _context.SubSkus.AsNoTracking()
                .Where(ss => ss.SkuId == sku.Id)
                .ToListAsync();

            foreach (var sub in subs)
            {
                sub.HistoryY0s = await _context.HistoryY0s.AsNoTracking()
                    .FirstOrDefaultAsync(h => h.SubSkuId == sub.Id) ?? new HistoryY0 { SubSkuId = sub.Id, Units = 0, Amount = 0m };

                sub.PlaningY1s = await _context.PlaningY1s.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.SubSkuId == sub.Id) ?? new PlaningY1 { SubSkuId = sub.Id, Units = 0, Amount = 0m };
            }

            sku.SubSkus = [.. subs];
            return sku;
        }
    }
}