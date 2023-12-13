namespace LStorage.Locations
{
    public class AllocateLocationContext
    {
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
