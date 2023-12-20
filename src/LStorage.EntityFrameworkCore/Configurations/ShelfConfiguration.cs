using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LStorage.EntityFrameworkCore.Configurations
{
    internal class ShelfConfiguration : IEntityTypeConfiguration<Shelf>
    {
        public void Configure(EntityTypeBuilder<Shelf> builder)
        {
            builder.ToTable("Shelves");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128).HasComment("Id");
            builder.Property(x => x.Code).HasMaxLength(256).HasComment("编码");
            builder.Property(x => x.AreaId).HasMaxLength(128).HasComment("区域Id");
            builder.Property(x => x.Type).HasComment("货架类型");
            builder.Property(x => x.IOType).HasComment("存储方式类型");
        }
    }
}
