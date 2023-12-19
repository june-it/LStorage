namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存排序参数
    /// </summary>
    public class AllocateInventorySorting
    {
        public AllocateInventorySorting(AllocateInventorySortingDimension dimension, AllocateInventorySortingDirection direction)
        {
            Dimension = dimension;
            Direction = direction;
        }
        public AllocateInventorySortingDimension Dimension { get; }
        public AllocateInventorySortingDirection Direction { get; }
    }

    /// <summary>
    /// 表示库存排序维度
    /// </summary>
    public enum AllocateInventorySortingDimension
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        InboundTime,
        /// <summary>
        /// 数量
        /// </summary>
        Qty,
        /// <summary>
        /// 所在库位的排序号
        /// </summary>
        Row,
        /// <summary>
        /// 所在库位的列序号
        /// </summary>
        Column,
        /// <summary>
        /// 所在库位的层序号
        /// </summary>
        Layer,
        /// <summary>
        /// 所在库位的深序号
        /// </summary>
        Depth,
    }
    /// <summary>
    /// 表示库存排序方向
    /// </summary>
    public enum AllocateInventorySortingDirection
    {
        /// <summary>
        /// 升序
        /// </summary>
        Ascending = 1,
        /// <summary>
        /// 降序
        /// </summary>
        Descending = 2,
    }
}
