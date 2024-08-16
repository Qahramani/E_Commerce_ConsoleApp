using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.ProductDtos;

public class ProductGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }

    public override string ToString()
    {
        return $"";
    }
}
