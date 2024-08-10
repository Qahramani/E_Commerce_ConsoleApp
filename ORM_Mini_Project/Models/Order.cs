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

}
