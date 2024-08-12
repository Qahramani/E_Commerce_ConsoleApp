using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(o => o.Quantity).IsRequired(true);
        builder.Property(o => o.PricePerItem).IsRequired(true).HasColumnType("decimal(8,2)");

        builder.Property(o => o.OrderId).IsRequired(true);
        builder.Property(o => o.ProductId).IsRequired(true);

    }
}
