using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    /// <summary>
    /// 表示穿梭式货架库位分配服务
    /// </summary>
    public class PalletShuttleRackingLocationAllocator : LocationAllocatorBase
    {
        public PalletShuttleRackingLocationAllocator(IServiceProvider serviceProvider) : base(serviceProvider) { }
        public override ShelfType[] ShelfTypes => new[] { ShelfType.PalletShuttleRacking };

        public override async Task<IList<Location>> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default)
        {
            if (!ShelfTypes.Contains(context.ToShelf.ShelfType))
            {
                throw new InvalidOperationException($"货架库位分配服务{GetType().Name}不支持类型{context.ToShelf.ShelfType}货架库位分配。");
            }
            // 如果未指定排/列/层/深且为内部分配库位时，推荐距离最近的货位（指定库位所在列、所在层）
            if (context.ToShelf.Code == context.FromShelf.Code
                && !context.Input.Row.HasValue
                && !context.Input.Column.HasValue
                && !context.Input.Layer.HasValue
                && !context.Input.Depth.HasValue)
            {
                context.Input.Layer = context.FromLocation.RCLD.Layer;
                context.Input.Column = context.FromLocation.RCLD.Column;
            }
            // 获取货架上的空库位
            var locations = DbProvider.GetAllLocations()
                .Where(x => x.ShelfCode == context.ToShelf.Code && x.StockCount == 0).ToList();

            // 筛选依赖深库位
            locations = FilterByDependencyLocations(locations, context.ToShelf.IOType);

            // 对排/列/层/深进行评分
            var locationsByIntegral = IntegralCalculation(locations, context.FromLocation.RCLD, context.Input.Row, context.Input.Column, context.Input.Layer, context.Input.Depth);

            // 对未评分的库位进行排序
            var exceptedLocations = SortingLocations(locations.Except(locationsByIntegral).ToList(), context.Input.SortingItems);

            // 合并数组
            var orderByLocations = locationsByIntegral.Union(exceptedLocations).ToList();

            return await Task.FromResult(orderByLocations
                     .Take(context.Input.Count)
                     .ToList());
        }
        /// <summary>
        /// 排除深依赖库位
        /// </summary>
        /// <param name="locations">原库位列表</param> 
        /// <param name="shelfIOType">货架存取类型</param>
        /// <returns></returns>
        private List<Location> FilterByDependencyLocations(List<Location> locations, ShelfIOType shelfIOType)
        {
            var availableLocations = new List<Location>();
            foreach (var location in locations)
            {
                switch (shelfIOType)
                {
                    case ShelfIOType.FILO:
                        // 深位小于货位的深位不能出现满库位
                        if (!locations.Any(x => x.RCLD.Depth < location.RCLD.Depth && x.StockCount == 1))
                        {
                            availableLocations.Add(location);
                        }
                        break;
                    case ShelfIOType.FIFO:
                    case ShelfIOType.BSIO:
                        // 货位深位两侧不能同时出现满库位
                        if (!(locations.Any(x => x.RCLD.Depth < location.RCLD.Depth && x.StockCount == 1) && locations.Any(x => x.RCLD.Depth > location.RCLD.Depth && x.StockCount == 1)))
                        {
                            availableLocations.Add(location);
                        }
                        break;
                }
            }
            return availableLocations;
        }

        /// <summary>
        /// 根据排/列/层/深进行积分并排序
        /// </summary>
        /// <param name="locations"></param>
        /// <param name="fromLocationRCLD"></param> 
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="layer"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private List<Location> IntegralCalculation(List<Location> locations, LocationRCLD fromLocationRCLD, int? row, int? column, int? layer, int? depth)
        {
            List<Location> orderByLocations = new List<Location>();
            Dictionary<Location, RCLDIntegral> integralItems = new Dictionary<Location, RCLDIntegral>();
            foreach (var location in locations)
            {
                RCLDIntegral integral = new RCLDIntegral();
                if (row.HasValue)
                {
                    integral.RowValue = Math.Abs(location.RCLD.Row - row.Value);
                }
                if (column.HasValue)
                {
                    integral.ColumnValue = Math.Abs(location.RCLD.Column - column.Value);
                }
                if (layer.HasValue)
                {
                    integral.RowValue = Math.Abs(location.RCLD.Layer - layer.Value);
                }
                if (depth.HasValue)
                {
                    integral.DepthValue = Math.Abs(location.RCLD.Depth - depth.Value);
                }
            }


            // 层
            var orderByLayerLocations = integralItems.GroupBy(x => x.Value.LayerValue).OrderBy(x => x.Key);
            foreach (var orderByLayerLocation in orderByLayerLocations)
            {
                // 列（排除同排同列）
                var orderByColumnLocations = orderByLayerLocation
                    .Where(x => !(x.Value.Location.RCLD.Row == fromLocationRCLD.Row && x.Value.Location.RCLD.Column == fromLocationRCLD.Column))
                    .GroupBy(x => x.Value.ColumnValue).OrderBy(x => x.Key);
                foreach (var orderByColumnLocation in orderByColumnLocations)
                {
                    // 排
                    var orderByRowLocations = orderByColumnLocation.GroupBy(x => x.Value.RowValue).OrderBy(x => x.Key);
                    foreach (var orderByRowLocation in orderByRowLocations)
                    {
                        // 深 
                        var orderByDepthLocations = orderByColumnLocation.GroupBy(x => x.Value.DepthValue).OrderByDescending(x => x.Key);
                        foreach (var orderByDepthLocation in orderByDepthLocations)
                        {
                            foreach (var integralItem in orderByDepthLocation)
                            {
                                orderByLocations.Add(integralItem.Key);
                            }
                        }
                    }
                }
            }
            return orderByLocations;
        }

        private List<Location> SortingLocations(List<Location> locations, AllocateLocationSorting[] sortingItems)
        {
            IOrderedEnumerable<Location> query = locations.OrderBy(x => x.RCLD.Layer);
            for (int i = 0; i < sortingItems.Length; i++)
            {
                if (i == 0)
                {
                    switch (sortingItems[i].RCLD)
                    {
                        case AllocateLocationRCLD.Row:
                            switch (sortingItems[i].Sorting)
                            {
                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Row);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Row);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Column:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Column);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Layer:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Layer);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Depth:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Depth);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    switch (sortingItems[i].RCLD)
                    {
                        case AllocateLocationRCLD.Row:
                            switch (sortingItems[i].Sorting)
                            {
                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Row);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Row);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Column:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Column);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Layer:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Layer);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Depth:
                            switch (sortingItems[i].Sorting)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Depth);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }

            }
            return query.ToList();
        }
    }
    public class RCLDIntegral
    {
        public Location Location { get; set; }
        public int? RowValue { get; set; }
        public int? ColumnValue { get; set; }
        public int? LayerValue { get; set; }
        public int? DepthValue { get; set; }
    }
}
