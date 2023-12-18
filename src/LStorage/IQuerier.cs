using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LStorage
{
    public interface IQuerier<TModel>
        where TModel : class, IModel
    {
        IQueryable<TModel> GetAll();
        Task<IList<TModel>> GetListAsync(Func<TModel, bool> predicate, Func<TModel, string> orderByKeySelector = null);
        Task<TModel> GetAsync(string code);
    }
}
