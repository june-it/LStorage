namespace LStorage
{
    /// <summary>
    /// 表示库位信息
    /// </summary>
    public class Location : IModel
    {
        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 获取或设置货架编码
        /// </summary>
        public virtual string ShelfCode { get; set; }
        /// <summary>
        /// 获取或设置所在的区域编码
        /// </summary>
        public virtual string AreaCode { get; set; }
        /// <summary>
        /// 获取或设置所在的排列层深
        /// </summary>
        public virtual LocationRCLD RCLD { get; set; }
        /// <summary>
        /// 获取或设置最多库存数量
        /// </summary> 
        public virtual int MaxStockCount { get; set; }
        /// <summary>
        /// 获取或设置库存数量
        /// </summary>
        public virtual int StockCount { get; set; }
    }

}
