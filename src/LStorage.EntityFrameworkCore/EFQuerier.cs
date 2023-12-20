using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LStorage.EntityFrameworkCore
{
    public class EFQuerier<TModel> : IQuerier<TModel>
           where TModel : class, IModel
    {
        private readonly LStorageDbContext _dbContext;
        public EFQuerier(LStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual IQueryable<TModel> GetAll()
        {
            return _dbContext.Set<TModel>().AsQueryable();
        }
        public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetAll().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<IList<TModel>> GetListAsync(Expression<Func<TModel, bool>> predicate, Func<TModel, string> orderByKeySelector = null, CancellationToken cancellationToken = default)
        {
            return await GetAll().Where(predicate)
                .ToListAsync(cancellationToken);
        }
    }
}
