using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.PaymentDtos;

public class PaymentExportDto
{
    public Order Order { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
