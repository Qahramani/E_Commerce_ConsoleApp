using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDtos;

public class OrderPostDto
{
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}
