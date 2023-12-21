using System.Collections.Generic;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存分配的输出结果
    /// </summary>
    public class AllocateInventoryOutput
    {
        /// <summary>
        /// 初始化一个库存分配的输出结果
        /// </summary>
        /// <param name="location">库位信息</param>
        /// <param name="inventory">库存信息</param>
        public AllocateInventoryOutput(Location location, Inventory inventory)
        {
            Location = location;
            Inventory = inventory;
            DependentLocations = new List<Location>();
        }
        /// <summary>
        /// 获取库位信息
        /// </summary>
        public Location Location { get; }
        /// <summary>
        /// 获取库存信息
        /// </summary>
        public Inventory Inventory { get; }
        /// <summary>
        /// 获取依赖库位信息
        /// </summary>
        public List<Location> DependentLocations { get; }
    }
}
