namespace LStorage.Locations
{
    /// <summary>
    /// 表示库位的分配结果
    /// </summary>
    public class AllocateLocationOutput
    {
        public AllocateLocationOutput(Location location)
        {
            Location = location;
        }
        public Location Location { get; }
    }
}
