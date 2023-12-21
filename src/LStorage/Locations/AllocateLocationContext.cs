namespace LStorage.Locations
{
    /// <summary>
    /// 表示分配库位的上下文数据
    /// </summary>
    public class AllocateLocationContext
    {
        /// <summary>
        /// 初始化分配库位的上下文数据
        /// </summary>
        /// <param name="fromArea">来源区域</param>
        /// <param name="fromShelf">来源货架</param>
        /// <param name="fromLocation">来源库位</param>
        /// <param name="toArea">目标区域</param>
        /// <param name="toShelf">目标货架</param>
        /// <param name="input">分配参数</param>
        public AllocateLocationContext(Area fromArea, Shelf fromShelf, Location fromLocation, Area toArea, Shelf toShelf, AllocateLocationInput input)
        {
            FromArea = fromArea;
            FromShelf = fromShelf;
            FromLocation = fromLocation;
            ToArea = toArea;
            ToShelf = toShelf;
            Input = input;
        }


        /// <summary>
        /// 获取来源区域信息
        /// </summary>
        public Area FromArea { get; }
        /// <summary>
        /// 获取来源货架信息
        /// </summary>
        public Shelf FromShelf { get; }
        /// <summary>
        /// 获取原始库位信息
        /// </summary>
        public Location FromLocation { get; }
        /// <summary>
        /// 获取目标区域信息
        /// </summary>
        public Area ToArea { get; }
        /// <summary>
        /// 获取目标货架信息
        /// </summary>
        public Shelf ToShelf { get; }
        /// <summary>
        /// 获取分配库位参数
        /// </summary>
        public AllocateLocationInput Input { get; }
    }
}
