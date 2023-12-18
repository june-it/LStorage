namespace LStorage
{
    /// <summary>
    /// 表示货架信息
    /// </summary>
    public class Shelf : IModel
    {
        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 获取或设置区域编码
        /// </summary>
        public virtual string AreaCode { get; set; }
        /// <summary>
        /// 获取或设置货架类型
        /// </summary>
        public virtual ShelfType ShelfType { get; set; }
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
        FIFO,
        /// <summary>
        /// 先进后出
        /// </summary>
        /// <remarks>
        /// 例如：山特立库、东方自控立库
        /// </remarks>
        FILO,
        /// <summary>
        /// 两侧进出
        /// </summary>
        /// <remarks>
        /// 例如：东江立库
        /// </remarks>
        BSIO
    }


}
