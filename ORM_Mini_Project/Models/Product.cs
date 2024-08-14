using ORM_Mini_Project.Models.Common;

namespace ORM_Mini_Project.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new();

}
