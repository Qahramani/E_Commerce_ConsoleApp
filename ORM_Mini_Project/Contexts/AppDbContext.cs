using Microsoft.EntityFrameworkCore;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Utilities;

namespace ORM_Mini_Project.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Constants.connectionString);
        base.OnConfiguring(optionsBuilder);
    }
}
