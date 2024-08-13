using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.PaymentDtos;

public class PaymentPostDto
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
}
