using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models.Common;

namespace ORM_Mini_Project.Models;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderDetail> OrderDetails { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();

    public override string ToString()
    {
        return $"OrderDate : {OrderDate}, TotalAmount : {TotalAmount}, Status : {Status}";
    }
}
