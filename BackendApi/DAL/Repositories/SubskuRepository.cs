using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq.Expressions;

namespace Intfastructure.Repositories
{
    public class SubskuRepository(BackendDbContext context) : ISubskuRepository
    {
        private readonly BackendDbContext _context = context;

        public IQueryable<SubSku> GetAll(Expression<Func<SubSku, bool>>? filter = null, bool includeDependencies = false)
        {
            // start from DbSet
            IQueryable<SubSku> query = _context.SubSkus;

            // apply filter first to restrict results
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (includeDependencies)
            {
                // include navigation properties and return tracked entities
                query = query
                    .Include(x => x.PlaningY1s);
                    // If you need HistoryY0s too, add: .Include(x => x.HistoryY0s);

                // returning the query as-is leaves entities tracked so changes to
                // navigation properties will be detected by SaveChangesAsync().
                return query;
            }

            // read-only branch - return no-tracking for better perf
            return query.AsNoTracking();
        }

        public void Update(int id, SubSku subsku)
        {
            _context.Update(subsku);
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}