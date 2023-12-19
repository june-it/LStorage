namespace LStorage
{
    /// <summary>
    /// 表示托盘信息
    /// </summary>
    public class Pallet : IModel
    {
        /// <summary>
        /// 表示初始化托盘信息
        /// </summary>
        public Pallet()
        {

        }
        /// <summary>
        /// 表示初始化托盘信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">编码</param>
        /// <param name="locationId">库位Id</param>
        public Pallet(string id, string code, string locationId)
        {
            Id = id;
            Code = code;
            LocationId = locationId;
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
        /// 获取或设置库位Id
        /// </summary>
        public virtual string LocationId { get; set; }
    }
}
