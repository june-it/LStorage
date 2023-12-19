namespace LStorage.Locations
{
    /// <summary>
    /// 表示库位的分配结果
    /// </summary>
    public class AllocateLocationResult
    {
        public AllocateLocationResult(Location location)
        {
            Location = location;
        }
        public Location Location { get; }
    }
}
