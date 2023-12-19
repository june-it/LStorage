using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage
{
    public abstract class QuerierBase<TModel> : IQuerier<TModel>
        where TModel : class, IModel
    {
        public abstract IQueryable<TModel> GetAll();
        public virtual Task<TModel> GetAsync(Func<TModel, bool> predicate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(GetAll().FirstOrDefault(predicate));
        }
        public async virtual Task<IList<TModel>> GetListAsync(Func<TModel, bool> predicate, Func<TModel, string> orderByKeySelector = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (orderByKeySelector == null)
            {
                return await Task.FromResult(GetAll()
                .Where(predicate)
                .ToList());
            }
            else
            {
                return await Task.FromResult(GetAll()
                .Where(predicate)
                .OrderBy(orderByKeySelector)
                .ToList());
            };
        }
    }
}
