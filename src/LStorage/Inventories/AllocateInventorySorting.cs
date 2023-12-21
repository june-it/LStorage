namespace LStorage.Inventories
{
    /// <summary>
    /// 表示分配库存排序
    /// </summary>
    public class AllocateInventorySorting
    {
        /// <summary>
        /// 初始化一个分配库存排序
        /// </summary>
        /// <param name="dimension">排序维度</param>
        /// <param name="direction">排序方向</param>
        public AllocateInventorySorting(AllocateInventorySortingDimension dimension, AllocateInventorySortingDirection direction)
        {
            Dimension = dimension;
            Direction = direction;
        }
        /// <summary>
        /// 获取分配库存排序维度
        /// </summary>
        public AllocateInventorySortingDimension Dimension { get; }
        /// <summary>
        /// 获取分配库存排序方向
        /// </summary>
        public AllocateInventorySortingDirection Direction { get; }
    }

    /// <summary>
    /// 表示分配库存排序维度
    /// </summary>
    public enum AllocateInventorySortingDimension
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        InboundTime,
        /// <summary>
        /// 库存数量
        /// </summary>
        Qty,
        /// <summary>
        /// 依赖数量
        /// </summary>
        DependentCount,
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
    /// 表示分配库存排序方向
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
