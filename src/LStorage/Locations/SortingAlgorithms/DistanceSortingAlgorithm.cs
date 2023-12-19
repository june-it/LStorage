using System;
using System.Collections.Generic;
using System.Linq;

namespace LStorage.Locations.SortingAlgorithms
{
    /// <summary>
    /// 最近距离算法
    /// </summary>
    public class DistanceSortingAlgorithm
    {
        public IList<Location> Sort(IList<Location> locations, LocationRCLD locationRCLD, AllocateLocationSortingDimension[] soringItems)
        {
            if (locations == null) throw new ArgumentNullException(nameof(locations), "目标库位列表不能为空。");
            if (soringItems == null) throw new ArgumentNullException(nameof(soringItems), "排序项不能为空。");

            Dictionary<Location, DistanceSortingValue> sortingValueLocations = new Dictionary<Location, DistanceSortingValue>();
            foreach (var location in locations)
            {
                DistanceSortingValue sortingValue = new DistanceSortingValue();
                if (locationRCLD.Row != 0)
                {
                    sortingValue.RowValue = Math.Abs(location.RCLD.Row - locationRCLD.Row);
                }
                else
                {
                    sortingValue.RowValue = location.RCLD.Row;
                }
                if (locationRCLD.Column != 0)
                {
                    sortingValue.ColumnValue = Math.Abs(location.RCLD.Column - locationRCLD.Column);
                }
                else
                {
                    sortingValue.ColumnValue = location.RCLD.Column;
                }
                if (locationRCLD.Layer != 0)
                {
                    sortingValue.LayerValue = Math.Abs(location.RCLD.Layer - locationRCLD.Layer);
                }
                else
                {
                    sortingValue.LayerValue = location.RCLD.Layer;
                }
                if (locationRCLD.Depth != 0)
                {
                    sortingValue.DepthValue = Math.Abs(location.RCLD.Depth - locationRCLD.Depth);
                }
                else
                {
                    sortingValue.DepthValue = -location.RCLD.Depth;
                }
                sortingValueLocations.Add(location, sortingValue);
            }
            List<Location> soringLocations = new List<Location>();
            Recursively(soringLocations, sortingValueLocations.GroupBy(x => x.Value.IrrelevantValue), -1, soringItems);
            return soringLocations;
        }
        private void Recursively(List<Location> soringLocations, IEnumerable<IGrouping<int, KeyValuePair<Location, DistanceSortingValue>>> groupingLocations, int currentLevel, AllocateLocationSortingDimension[] allocateLocationRCLDs)
        {
            currentLevel += 1;
            foreach (var groupingLocation in groupingLocations)
            {
                var subGroupingLocations = groupingLocation.GroupBy(x => x.Value.LayerValue).OrderBy(x => x.Key);
                switch (allocateLocationRCLDs[currentLevel])
                {
                    case AllocateLocationSortingDimension.Row:
                        subGroupingLocations = groupingLocation.GroupBy(x => x.Value.RowValue).OrderBy(x => x.Key);
                        break;
                    case AllocateLocationSortingDimension.Column:
                        subGroupingLocations = groupingLocation.GroupBy(x => x.Value.ColumnValue).OrderBy(x => x.Key);
                        break;
                    case AllocateLocationSortingDimension.Layer:
                        subGroupingLocations = groupingLocation.GroupBy(x => x.Value.LayerValue).OrderBy(x => x.Key);
                        break;
                    case AllocateLocationSortingDimension.Depth:
                        subGroupingLocations = groupingLocation.GroupBy(x => x.Value.DepthValue).OrderBy(x => x.Key);
                        break;
                    default:
                        break;
                }

                if (currentLevel == allocateLocationRCLDs.Length - 1)
                {
                    foreach (var subGroupingLocation in subGroupingLocations)
                    {
                        soringLocations.AddRange(subGroupingLocation.Select(x => x.Key).ToList());
                    }
                }
                else
                    Recursively(soringLocations, subGroupingLocations, currentLevel, allocateLocationRCLDs);
            }
        }

        class DistanceSortingValue
        {
            public DistanceSortingValue()
            {
                IrrelevantValue = 0;
            }
            public int RowValue { get; set; }
            public int ColumnValue { get; set; }
            public int LayerValue { get; set; }
            public int DepthValue { get; set; }
            public int IrrelevantValue { get; }
        }
    }
}
