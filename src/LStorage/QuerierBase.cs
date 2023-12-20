using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage
{
    public abstract class QuerierBase<TModel> : IQuerier<TModel>
        where TModel : class, IModel
    {
        public abstract IQueryable<TModel> GetAll();
        public virtual Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(GetAll().Where(predicate).FirstOrDefault());
        }
        public async virtual Task<IList<TModel>> GetListAsync(Expression<Func<TModel, bool>> predicate, Func<TModel, string> orderByKeySelector = null, CancellationToken cancellationToken = default)
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
