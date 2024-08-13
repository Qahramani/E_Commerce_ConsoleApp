using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDetailDtos;

public class OrderDetailPostDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
}
