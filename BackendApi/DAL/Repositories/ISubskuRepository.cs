using Models;
using System.Linq.Expressions;

namespace Intfastructure.Repositories
{
    public interface ISubskuRepository
    {
        IQueryable<SubSku> GetAll(Expression<Func<SubSku, bool>>? filter = null, bool includeDependencies = false);
        void Update(int id, SubSku subsku);
        Task SaveAsync();
    }
}