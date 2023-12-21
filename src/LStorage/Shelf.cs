namespace LStorage
{
    /// <summary>
    /// 表示货架信息
    /// </summary>
    public class Shelf : IModel
    {
        /// <summary>
        /// 表示初始化货架信息
        /// </summary>
        public Shelf()
        {

        }
        /// <summary>
        /// 表示初始化货架信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">编码</param>
        /// <param name="areaId">区域Id</param>
        /// <param name="shelfType">货架类型</param>
        /// <param name="iOType">货架存储方式</param>
        public Shelf(string id, string code, string areaId, ShelfType shelfType, ShelfIOType ioType)
        {
            Id = id;
            Code = code;
            AreaId = areaId;
            Type = shelfType;
            IOType = ioType;
        }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 获取或设置区域Id
        /// </summary>
        public virtual string AreaId { get; set; }
        /// <summary>
        /// 获取或设置货架类型
        /// </summary>
        public virtual ShelfType Type { get; set; }
        /// <summary>
        /// 获取或设置存取方式
        /// </summary>
        public virtual ShelfIOType IOType { get; set; }
    }

    public enum ShelfIOType
    {
        /// <summary>
        /// 先进先出
        /// </summary>
        /// <remarks>
        /// 例如：东江立库
        /// </remarks>
        FIFO,
        /// <summary>
        /// 先进后出
        /// </summary>
        /// <remarks>
        /// 例如：山特立库、东方自控立库
        /// </remarks>
        FILO
    }
    public enum ShelfType
    {
        /// <summary>
        /// 单层货架
        /// </summary>
        SingleLayer = 0,
        /// <summary>
        /// 单层地堆货架
        /// </summary>
        SingleLayerStack = 1,
        /// <summary>
        /// 穿梭式货架 
        /// </summary> 
        PalletShuttleRacking = 2,
    }

}
