using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.TotalAmount).IsRequired().HasDefaultValue(0).HasColumnType("decimal(10,2)");
        
        builder.Property(o => o.UserId).IsRequired(true);
        builder.Property(o => o.Status).IsRequired(true).HasDefaultValue(1);
    }
}
