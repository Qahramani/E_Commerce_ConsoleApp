using ORM_Mini_Project.Models.Common;

namespace ORM_Mini_Project.Models; 

public class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int MyProperty { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }

}
