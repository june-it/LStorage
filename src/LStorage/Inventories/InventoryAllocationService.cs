using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    public class InventoryAllocationService : IInventoryAllocationService
    {
        private readonly IQuerier<Material> _materialQuerier;
        private readonly IQuerier<Inventory> _inventoryQuerier;
        private readonly IQuerier<Pallet> _palletQuerier;
        private readonly IQuerier<Location> _locationQuerier;
        private readonly IQuerier<Area> _areaQuerier;
        private readonly IQuerier<Shelf> _shelfQuerier;
        public InventoryAllocationService(IServiceProvider serviceProvider)
        {
            _materialQuerier = serviceProvider.GetRequiredService<IQuerier<Material>>();
            _inventoryQuerier = serviceProvider.GetRequiredService<IQuerier<Inventory>>();
            _palletQuerier = serviceProvider.GetRequiredService<IQuerier<Pallet>>();
            _locationQuerier = serviceProvider.GetRequiredService<IQuerier<Location>>();
            _areaQuerier = serviceProvider.GetRequiredService<IQuerier<Area>>();
            _shelfQuerier = serviceProvider.GetRequiredService<IQuerier<Shelf>>();
        }
        public async Task<IList<AllocateInventoryResult>> AllocateAsync(AllocateInventoryInput input, CancellationToken cancellationToken = default)
        {
            if (input == null) throw new ArgumentNullException(nameof(input), "参数不能为空。");
            if (string.IsNullOrEmpty(input.MaterialCode)) throw new ArgumentNullException(nameof(input.MaterialCode), "物料编码不能为空。");
            if (input.Qty <= 0) throw new ArgumentNullException(nameof(input.MaterialCode), "分配数量不能少于0。");

            var material = await _materialQuerier.GetAsync(x => x.Code == input.MaterialCode, cancellationToken) ?? throw new ArgumentException($"物料{input.MaterialCode}信息不存在。");

            var query = from a in _inventoryQuerier.GetAll()
                        join b in _palletQuerier.GetAll() on a.PalletId equals b.Id
                        join c in _locationQuerier.GetAll() on b.LocationId equals c.Id
                        where a.MaterialId == material.Id
                        select new AllocateInventoryResult(c, a);
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
            var inventories = query.ToList();
            if (inventories == null || inventories.Sum(x => x.Inventory.Qty) < input.Qty)
            {
                throw new InvalidOperationException($"物料{input.MaterialCode}库存不足。");
            }
            // 进行结果排序
            var orderedByResult = Sort(inventories, input.SortingItems);
            // 按数量分配库存
            int needQty = input.Qty;
            for (int i = 0; i < orderedByResult.Count; i++)
            {
                needQty -= orderedByResult[i].Inventory.Qty;
                if (needQty <= 0)
                    return orderedByResult.Take(i + 1).ToList();
            }
            return default;
        }
        protected virtual List<AllocateInventoryResult> Sort(IList<AllocateInventoryResult> result, AllocateInventorySorting[] sortingItems)
        {
            if (sortingItems == null)
                return (List<AllocateInventoryResult>)result;
            IOrderedEnumerable<AllocateInventoryResult> query = result.OrderBy(x => x.Location.Id);
            for (int i = 0; i < sortingItems.Length; i++)
            {
                if (i == 0)
                {
                    switch (sortingItems[i].Dimension)
                    {
                        case AllocateInventorySortingDimension.InboundTime:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Inventory.InboundTime);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Inventory.InboundTime);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Qty:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Inventory.Qty);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Inventory.Qty);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Row:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Layer);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Depth);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    switch (sortingItems[i].Dimension)
                    {
                        case AllocateInventorySortingDimension.InboundTime:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Inventory.InboundTime);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Inventory.InboundTime);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Qty:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Inventory.Qty);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Inventory.Qty);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Row:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Layer);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Depth);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }

            }
            return query.ToList();
        }
    }


}
