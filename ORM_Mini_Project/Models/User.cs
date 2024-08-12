using ORM_Mini_Project.Models.Common;

namespace ORM_Mini_Project.Models;

public class User : BaseEntity
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
}
