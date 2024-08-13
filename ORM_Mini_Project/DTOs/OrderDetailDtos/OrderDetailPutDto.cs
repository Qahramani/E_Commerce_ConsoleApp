using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDetailDtos;

public class OrderDetailPutDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}
