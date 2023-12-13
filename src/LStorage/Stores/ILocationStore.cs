using System.Linq;

namespace LStorage.Stores
{
    public interface ILocationStore
    {
        IQueryable<Location> GetAll();
    }
}
