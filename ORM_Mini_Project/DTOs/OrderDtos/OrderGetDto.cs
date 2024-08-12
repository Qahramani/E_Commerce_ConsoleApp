using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDtos;

public class OrderGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public List<Payment> Payments { get; set; }
}
