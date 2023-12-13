using System.Linq;
using System.Threading.Tasks;

namespace LStorage
{
    public interface IDbProvider
    {
        IQueryable<Area> GetAllAreas();
        Task<Area> GetAreaAsync(string code);
        IQueryable<Location> GetAllLocations();
        Task<Location> GetLocationAsync(string code);
        IQueryable<Shelf> GetShelves();
        Task<Shelf> GetShelfAsync(string code);
    }
}
