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
        /// 获取或设置分配排序号
        /// </summary>
        public int? Row { get; set; }
        /// <summary>
        /// 获取或设置分配列序号
        /// </summary>
        public int? Column { get; set; }
        /// <summary>
        /// 获取或设置分配层序号
        /// </summary>
        public int? Layer { get; set; }
        /// <summary>
        /// 获取或设置分配深序号
        /// </summary>
        public int? Depth { get; set; }
        /// 获取或设置库位排序方式
        /// </summary>
        public AllocateLocationSorting[] SortingItems { get; set; } =
            new AllocateLocationSorting[]
            {
                new AllocateLocationSorting( AllocateLocationRCLD.Layer, AllocateSortingDirection.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Column, AllocateSortingDirection.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Row, AllocateSortingDirection.Ascending),
                new AllocateLocationSorting( AllocateLocationRCLD.Depth, AllocateSortingDirection.Descending)
        };
    }


}
