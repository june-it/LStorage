using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LStorage.EntityFrameworkCore.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128).HasComment("Id");
            builder.Property(x => x.Code).HasMaxLength(256).HasComment("编码");
            builder.Property(x => x.AreaId).HasMaxLength(128).HasComment("区域Id");
            builder.Property(x => x.ShelfId).HasMaxLength(128).HasComment("货架Id"); 
            builder.OwnsOne(p => p.RCLD).Property(p => p.Row).HasColumnName("排序号");
            builder.OwnsOne(p => p.RCLD).Property(p => p.Column).HasColumnName("列序号");
            builder.OwnsOne(p => p.RCLD).Property(p => p.Layer).HasColumnName("层序号");
            builder.OwnsOne(p => p.RCLD).Property(p => p.Depth).HasColumnName("深序号");
            builder.Property(x => x.MaxPalletCount).HasComment("最多托盘数量");
            builder.Property(x => x.PalletCount).HasComment("托盘数量");
        }
    }
}
