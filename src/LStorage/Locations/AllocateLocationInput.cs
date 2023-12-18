namespace LStorage.Locations
{
    /// <summary>
    /// 表示库位分配参数
    /// </summary>
    public class AllocateLocationInput
    {
        /// <summary>
        /// 获取或设置来源库位编码
        /// </summary>
        public string FromCode { get; set; }
        /// <summary>
        /// 获取或设置分配区域编码
        /// </summary>
        public string ToAreaCode { get; set; }
        /// <summary>
        /// 获取或设置分配货架编码
        /// </summary>
        public string ToShelfCode { get; set; }
        /// <summary>
        /// 获取或设置分配排
        /// </summary>
        public int? Row { get; set; }
        /// <summary>
        /// 获取或设置分配列
        /// </summary>
        public int? Column { get; set; }
        /// <summary>
        /// 获取或设置分配层
        /// </summary>
        public int? Layer { get; set; }
        /// <summary>
        /// 获取或设置分配深
        /// </summary>
        public int? Depth { get; set; }
        /// 获取或设置库位排序方式
        /// </summary>
        public AllocateLocationSorting[] SortingItems { get; set; } =
            new AllocateLocationSorting[]
            {
                new AllocateLocationSorting( AllocateLocationRCLD.Layer, Sorting.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Column, Sorting.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Row, Sorting.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Depth, Sorting.Descending)
        };
    }


}
