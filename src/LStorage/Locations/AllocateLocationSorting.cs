namespace LStorage.Locations
{

    /// <summary>
    /// 表示库位排序方式
    /// </summary> 
    public class AllocateLocationSorting
    {
        public AllocateLocationSorting(AllocateLocationRCLD rcld, Sorting sorting)
        {
            RCLD = rcld;
            Sorting = sorting;
        }
        public AllocateLocationRCLD RCLD { get; }
        public Sorting Sorting { get; }
    }
    public enum AllocateLocationRCLD
    {
        Row,
        Column,
        Layer,
        Depth,
    }
    public enum Sorting
    {
        Ascending = 1,
        Descending = 2,
    }


}
