using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage
{
    /// <summary>
    /// 表示数据模型查询器
    /// </summary>
    /// <typeparam name="TModel">数据模型类型</typeparam>
    public interface IQuerier<TModel>
        where TModel : class, IModel
    {
        /// <summary>
        /// 获取数据模型查询表达式树
        /// </summary>
        /// <returns></returns>
        IQueryable<TModel> GetAll();
        /// <summary>
        /// 获取数据模型列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderByKeySelector">排序条件</param>
        /// <param name="cancellationToken">取消令牌</param> 
        /// <returns></returns>
        Task<IList<TModel>> GetListAsync(Expression<Func<TModel, bool>> predicate, Func<TModel, string> orderByKeySelector = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条模型数据记录
        /// </summary>
        /// <param name="predicate">查询条件</param> 
        /// <param name="cancellationToken">取消令牌</param> 
        /// <returns></returns>
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
