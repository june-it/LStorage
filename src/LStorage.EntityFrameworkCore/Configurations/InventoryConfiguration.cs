using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LStorage.EntityFrameworkCore.Configurations
{
    internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128).HasComment("Id");
            builder.Property(x => x.MaterialId).HasMaxLength(128).HasComment("物料Id");
            builder.Property(x => x.PalletId).HasMaxLength(128).HasComment("托盘Id");
            builder.Property(x => x.Qty).HasComment("数量");
            builder.Property(x => x.InboundTime).HasComment("入库时间");
        }
    }
}
