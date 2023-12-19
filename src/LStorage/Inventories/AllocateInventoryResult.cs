namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存的分配结果
    /// </summary>
    public class AllocateInventoryResult
    {
        public AllocateInventoryResult(Location location, Inventory inventory)
        {
            Location = location;
            Inventory = inventory;
        }

        public Location Location { get; }
        public Inventory Inventory { get; }
    }
}
