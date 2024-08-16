using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.OrderDtos;

public class OrderExportDto
{
    public int Id { get; set; }
    public string UserName{ get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
   
}
