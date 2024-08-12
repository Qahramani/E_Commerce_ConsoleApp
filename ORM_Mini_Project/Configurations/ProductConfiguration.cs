using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired(true).HasMaxLength(128);
        builder.HasIndex(p => p.Name).IsUnique(true);
        builder.Property(p => p.Description).IsRequired(false).HasMaxLength(256);
        builder.Property(p => p.Price).IsRequired(true).HasColumnType("decimal(8,2)").HasDefaultValue(0);
        builder.Property(p => p.Stock).IsRequired(true).HasDefaultValue(0);

        builder.HasCheckConstraint("CK_Price", "Price >= 0");
        builder.HasCheckConstraint("CK_Stock", "Stock >= 0");
    }
}
