using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LStorage.EntityFrameworkCore.Configurations
{
    internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128).HasComment("Id");
            builder.Property(x => x.Code).HasMaxLength(256).HasComment("编码");
            builder.Property(x => x.Name).HasMaxLength(256).HasComment("名称");
           
        }
    }
}
