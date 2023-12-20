using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LStorage.EntityFrameworkCore.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128).HasComment("Id");
            builder.Property(x => x.Code).HasMaxLength(256).HasComment("编码");
        }
    }
}
