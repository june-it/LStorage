using System.Linq;

namespace LStorage.Stores
{
    public interface IShelfStore
    {
        IQueryable<Shelf> GetAll();
    }
}
