﻿namespace LStorage.Locations
{

    /// <summary>
    /// 表示库位排序
    /// </summary> 
    public class AllocateLocationSorting
    {
        public AllocateLocationSorting(AllocateLocationSortingDimension dimension, AllocateLocationSortingDirection direction)
        {
            Dimension = dimension;
            Direction = direction;
        }
        public AllocateLocationSortingDimension Dimension { get; }
        public AllocateLocationSortingDirection Direction { get; }
    }
    /// <summary>
    /// 表示库位排序维度
    /// </summary>
    public enum AllocateLocationSortingDimension
    {
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
    /// 表示库位排序方向
    /// </summary>
    public enum AllocateLocationSortingDirection
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
