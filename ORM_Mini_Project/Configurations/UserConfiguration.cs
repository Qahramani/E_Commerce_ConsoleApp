using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Fullname).IsRequired(true).HasMaxLength(128);
        builder.Property(u => u.Email).IsRequired(true).HasMaxLength(128);
        builder.HasIndex(u => u.Email).IsUnique(true);
        builder.Property(u => u.Password).IsRequired(true).HasMaxLength(256);
        builder.Property(u => u.Address).HasMaxLength(128);
    }
}
