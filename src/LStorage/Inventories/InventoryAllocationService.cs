using LStorage.Inventories.SortingAlgorithms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存分配服务
    /// </summary>
    public class InventoryAllocationService : IInventoryAllocationService
    {
        private readonly IQuerier<Material> _materialQuerier;
        private readonly IQuerier<Inventory> _inventoryQuerier;
        private readonly IQuerier<Pallet> _palletQuerier;
        private readonly IQuerier<Location> _locationQuerier;
        private readonly IQuerier<Area> _areaQuerier;
        private readonly IQuerier<Shelf> _shelfQuerier;
        private readonly InventorySortingAlgorithm _inventorySortingAlgorithm;
        // 缓存库位依赖查找器
        private static Dictionary<ShelfType, ILocationDependencyFinder> _cache = new Dictionary<ShelfType, ILocationDependencyFinder>();
        public InventoryAllocationService(IServiceProvider serviceProvider)
        {
            _materialQuerier = serviceProvider.GetRequiredService<IQuerier<Material>>();
            _inventoryQuerier = serviceProvider.GetRequiredService<IQuerier<Inventory>>();
            _palletQuerier = serviceProvider.GetRequiredService<IQuerier<Pallet>>();
            _locationQuerier = serviceProvider.GetRequiredService<IQuerier<Location>>();
            _areaQuerier = serviceProvider.GetRequiredService<IQuerier<Area>>();
            _shelfQuerier = serviceProvider.GetRequiredService<IQuerier<Shelf>>();
            _inventorySortingAlgorithm = serviceProvider.GetRequiredService<InventorySortingAlgorithm>();

            var inventoryDependencyFinders = serviceProvider.GetServices<ILocationDependencyFinder>();
            if (!_cache.Any())
            {
                foreach (var inventoryDependencyFinder in inventoryDependencyFinders)
                {
                    foreach (var shelfType in inventoryDependencyFinder.ShelfTypes)
                    {
                        if (!_cache.TryAdd(shelfType, inventoryDependencyFinder))
                            throw new InvalidOperationException($"{shelfType}已重复注册库存依赖服务。");
                    }
                }
            }
        }
        /// <summary>
        /// 分配库存
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回分配库存的结果集合</returns>
        public virtual async Task<IList<AllocateInventoryOutput>> AllocateAsync(AllocateInventoryInput input, CancellationToken cancellationToken = default)
        {
            if (input == null) throw new ArgumentNullException(nameof(input), "参数不能为空。");
            if (string.IsNullOrEmpty(input.MaterialCode)) throw new ArgumentNullException(nameof(input.MaterialCode), "物料编码不能为空。");
            if (input.Qty <= 0) throw new ArgumentNullException(nameof(input.MaterialCode), "分配数量不能少于0。");
            // 查询库存记录
            var items = await QueryInventoriesAsync(input, cancellationToken);

            // 检索库存依赖项
            await CheckInventoryDependencies(items, cancellationToken);

            // 进行结果排序
            var orderByItems = _inventorySortingAlgorithm.Sort(items, input.SortingItems);

            return AllocateInventoryItemsOnDemand(input.Qty, orderByItems);
        }
        /// <summary>
        /// 查询库存中的记录
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual async Task<IList<AllocateInventoryOutput>> QueryInventoriesAsync(AllocateInventoryInput input, CancellationToken cancellationToken)
        {
            var material = await _materialQuerier.GetAsync(x => x.Code == input.MaterialCode, cancellationToken) ?? throw new ArgumentException($"物料{input.MaterialCode}信息不存在。");

            var query = from a in _inventoryQuerier.GetAll()
                        join b in _palletQuerier.GetAll() on a.PalletId equals b.Id
                        join c in _locationQuerier.GetAll() on b.LocationId equals c.Id
                        where a.MaterialId == material.Id
                        select new AllocateInventoryOutput(c, a);
            if (!string.IsNullOrEmpty(input.AreaCode))
            {
                var area = await _areaQuerier.GetAsync(x => x.Code == input.AreaCode, cancellationToken) ?? throw new ArgumentException($"区域{input.AreaCode}信息不存在。");
                query = query.Where(x => x.Location.AreaId == area.Id);
            }
            if (!string.IsNullOrEmpty(input.ShelfCode))
            {
                var shelf = await _shelfQuerier.GetAsync(x => x.Code == input.ShelfCode, cancellationToken) ?? throw new ArgumentException($"货架{input.ShelfCode}信息不存在。");
                query = query.Where(x => x.Location.ShelfId == shelf.Id);
            }
            var items = query.ToList();
            if (items == null || items.Sum(x => x.Inventory.Qty) < input.Qty)
            {
                throw new InvalidOperationException($"物料{input.MaterialCode}库存不足。");
            }
            return items;
        }
        /// <summary>
        /// 检查库存依赖项
        /// </summary>
        /// <param name="items">分配库存记录</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual async Task CheckInventoryDependencies(IList<AllocateInventoryOutput> items, CancellationToken cancellationToken)
        {
            Dictionary<string, ShelfType> shelfCache = new Dictionary<string, ShelfType>();
            foreach (var item in items)
            {
                if (!shelfCache.TryGetValue(item.Location.ShelfId, out var shelfType))
                {
                    var shelf = await _shelfQuerier.GetAsync(x => x.Id == item.Location.ShelfId, cancellationToken) ?? throw new ArgumentException($"货架{item.Location.ShelfId}信息不存在。");
                    shelfCache.Add(shelf.Id, shelf.Type);
                    shelfType = shelf.Type;
                }
                if (!_cache.TryGetValue(shelfType, out var dependencyFinder))
                {
                    throw new InvalidOperationException($"{shelfType}未注册分配服务。");
                }
                item.DependentLocations.AddRange(await dependencyFinder.GetDependentLocationsAsync(item.Location, cancellationToken));
            }
        }
        /// <summary>
        /// 按数量分配库存 
        /// </summary>
        /// <param name="needQty">需要的库存数量</param>
        /// <param name="orderByItems">已排序的分配库存记录</param>
        /// <returns></returns>
        protected virtual IList<AllocateInventoryOutput> AllocateInventoryItemsOnDemand(int needQty, IList<AllocateInventoryOutput> orderByItems)
        {
            for (int i = 0; i < orderByItems.Count; i++)
            {
                needQty -= orderByItems[i].Inventory.Qty;
                if (needQty <= 0)
                    return orderByItems.Take(i + 1).ToList();
            }
            return default;
        }

    }


}
