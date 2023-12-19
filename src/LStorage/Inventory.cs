using System;

namespace LStorage
{
    /// <summary>
    /// 表示库存信息
    /// </summary>
    public class Inventory : IModel
    {
        /// <summary>
        /// 表示初始化库存信息
        /// </summary>
        public Inventory()
        {

        }
        /// <summary>
        /// 表示初始化库存信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="palletId">托盘Id</param>
        /// <param name="materialId">物料Id</param>
        /// <param name="qty">库存数量</param>
        /// <param name="inboundTime">入库时间</param>
        public Inventory(string id, string palletId, string materialId, int qty, DateTime inboundTime)
        {
            Id = id;
            PalletId = palletId;
            MaterialId = materialId;
            Qty = qty;
            InboundTime = inboundTime;
        }


        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 获取或设置托盘Id
        /// </summary>
        public virtual string PalletId { get; set; }
        /// <summary>
        /// 获取或设置物料Id
        /// </summary>
        public virtual string MaterialId { get; set; }
        /// <summary>
        /// 获取或设置库存数量
        /// </summary>
        public virtual int Qty { get; set; }
        /// <summary>
        /// 获取或设置入库时间
        /// </summary>
        public virtual DateTime InboundTime { get; set; }
    }
}
