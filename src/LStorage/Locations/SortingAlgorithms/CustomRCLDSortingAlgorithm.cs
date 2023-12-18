using System.Collections.Generic;
using System.Linq;

namespace LStorage.Locations.SortingAlgorithms
{
    /// <summary>
    /// 表示自定义RCLD排序算法
    /// </summary>
    public class CustomRCLDSortingAlgorithm
    {
        public List<Location> Sort(IList<Location> locations, AllocateLocationSorting[] sortingItems)
        {
            if (sortingItems == null)
                return (List<Location>)locations;
            IOrderedEnumerable<Location> query = locations.OrderBy(x => x.RCLD.Layer);
            for (int i = 0; i < sortingItems.Length; i++)
            {
                if (i == 0)
                {
                    switch (sortingItems[i].RCLD)
                    {
                        case AllocateLocationRCLD.Row:
                            switch (sortingItems[i].Direction)
                            {
                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Row);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Row);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Column);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Layer);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.OrderBy(x => x.RCLD.Depth);
                                    break;
                                case Sorting.Descending:
                                    query = query.OrderByDescending(x => x.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    switch (sortingItems[i].RCLD)
                    {
                        case AllocateLocationRCLD.Row:
                            switch (sortingItems[i].Direction)
                            {
                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Row);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Row);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Column);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Layer);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateLocationRCLD.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case Sorting.Ascending:
                                    query = query.ThenBy(x => x.RCLD.Depth);
                                    break;
                                case Sorting.Descending:
                                    query = query.ThenByDescending(x => x.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }

            }
            return query.ToList();
        }
    }
}
