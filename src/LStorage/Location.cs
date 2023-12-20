namespace LStorage
{
    /// <summary>
    /// 表示库位信息
    /// </summary>
    public class Location : IModel
    {
        /// <summary>
        /// 表示初始化库位信息
        /// </summary>
        public Location()
        {

        }
        /// <summary>
        /// 表示初始化库位信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">编码</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="areaId">区域Id</param>
        /// <param name="rcld">库位位置</param>
        /// <param name="maxPalletCount">最大库存数量</param>
        /// <param name="palletCount">当前库存数量</param>
        public Location(string id, string code, string shelfId, string areaId, LocationRCLD rcld, int maxPalletCount, int palletCount)
        {
            Id = id;
            Code = code;
            ShelfId = shelfId;
            AreaId = areaId;
            RCLD = rcld;
            MaxPalletCount = maxPalletCount;
            PalletCount = palletCount;
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
        /// 获取或设置货架Id
        /// </summary>
        public virtual string ShelfId { get; set; }
        /// <summary>
        /// 获取或设置所在的区域Id
        /// </summary>
        public virtual string AreaId { get; set; }
        /// <summary>
        /// 获取或设置所在的排列层深
        /// </summary>
        public virtual LocationRCLD RCLD { get; set; }
        /// <summary>
        /// 获取或设置最多托盘数量
        /// </summary> 
        public virtual int MaxPalletCount { get; set; }
        /// <summary>
        /// 获取或设置托盘数量
        /// </summary>
        public virtual int PalletCount { get; set; }
    }

    /// <summary>
    ///  表示库位的排列层深
    /// </summary>
    public class LocationRCLD
    {
        /// <summary>
        /// 获取或设置排序号
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 获取或设置列序号
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// 获取或设置层序号
        /// </summary>
        public int Layer { get; set; }
        /// <summary>
        /// 获取或设置深度序号
        /// </summary>
        public int Depth { get; set; }
    }

}
