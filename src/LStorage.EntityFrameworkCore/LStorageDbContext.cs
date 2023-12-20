using Microsoft.EntityFrameworkCore;

namespace LStorage.EntityFrameworkCore
{
    public class LStorageDbContext : DbContext
    { 
        public LStorageDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LStorageDbContext).Assembly);
        }
    }
}