using System.Collections.Generic;
using System.Linq;

namespace LStorage.Inventories.SortingAlgorithms
{
    public class InventorySortingAlgorithm
    {
        public List<AllocateInventoryOutput> Sort(IList<AllocateInventoryOutput> result, AllocateInventorySorting[] sortingItems)
        {
            if (sortingItems == null)
                return (List<AllocateInventoryOutput>)result;
            IOrderedEnumerable<AllocateInventoryOutput> query = result.OrderBy(x => x.Location.Id);
            for (int i = 0; i < sortingItems.Length; i++)
            {
                if (i == 0)
                {
                    switch (sortingItems[i].Dimension)
                    {
                        case AllocateInventorySortingDimension.InboundTime:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Inventory.InboundTime);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Inventory.InboundTime);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.DependentCount:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.DependentLocations.Count);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.DependentLocations.Count);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Qty:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Inventory.Qty);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Inventory.Qty);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Row:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Layer);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.OrderBy(x => x.Location.RCLD.Depth);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.OrderByDescending(x => x.Location.RCLD.Depth);
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    switch (sortingItems[i].Dimension)
                    {
                        case AllocateInventorySortingDimension.InboundTime:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Inventory.InboundTime);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Inventory.InboundTime);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.DependentCount:
                            switch (sortingItems[i].Direction)
                            {
                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.DependentLocations.Count);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.DependentLocations.Count);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Qty:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Inventory.Qty);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Inventory.Qty);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Row:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Column:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Column);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Column);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Layer:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Layer);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Layer);
                                    break;
                            }
                            break;
                        case AllocateInventorySortingDimension.Depth:
                            switch (sortingItems[i].Direction)
                            {

                                case AllocateInventorySortingDirection.Ascending:
                                    query = query.ThenBy(x => x.Location.RCLD.Depth);
                                    break;
                                case AllocateInventorySortingDirection.Descending:
                                    query = query.ThenByDescending(x => x.Location.RCLD.Depth);
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
