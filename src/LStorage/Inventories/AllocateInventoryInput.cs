namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存分配的输入参数
    /// </summary>
    public class AllocateInventoryInput
    {
        /// <summary>
        /// 获取或设置物料编号
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 获取或设置分配区域编码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 获取或设置分配货架编码
        /// </summary>
        public string ShelfCode { get; set; }
        /// <summary>
        /// 获取或设置分配库存数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 获取或设置库存排序方式
        /// </summary>
        public AllocateInventorySorting[] SortingItems { get; set; } =
            new AllocateInventorySorting[]
            {
                new AllocateInventorySorting( AllocateInventorySortingDimension.Layer, AllocateInventorySortingDirection.Ascending),
                new AllocateInventorySorting( AllocateInventorySortingDimension.Depth, AllocateInventorySortingDirection.Ascending),
                new AllocateInventorySorting( AllocateInventorySortingDimension.InboundTime, AllocateInventorySortingDirection.Ascending),
                new AllocateInventorySorting( AllocateInventorySortingDimension.Qty, AllocateInventorySortingDirection.Descending),
                new AllocateInventorySorting( AllocateInventorySortingDimension.DependentCount, AllocateInventorySortingDirection.Ascending)
        };
    }
}
