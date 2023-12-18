namespace LStorage.Locations
{

    /// <summary>
    /// 表示库位排序
    /// </summary> 
    public class AllocateLocationSorting
    {
        public AllocateLocationSorting(AllocateLocationRCLD rcld, AllocateSortingDirection direction)
        {
            RCLD = rcld;
            Direction = direction;
        }
        public AllocateLocationRCLD RCLD { get; }
        public AllocateSortingDirection Direction { get; }
    }
    /// <summary>
    /// 表示库位排序维度
    /// </summary>
    public enum AllocateLocationRCLD
    {
        Row,
        Column,
        Layer,
        Depth,
    }
    /// <summary>
    /// 表示库位排序方向
    /// </summary>
    public enum AllocateSortingDirection
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
