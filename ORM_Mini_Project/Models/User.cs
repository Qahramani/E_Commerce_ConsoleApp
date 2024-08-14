using ORM_Mini_Project.Models.Common;

namespace ORM_Mini_Project.Models;

public class User : BaseEntity
{
    public string Fullname { get; set; } = null!;
    public string Email { get; set; } = null!;      
    public string Password { get; set; } = null!;
    public string Address { get; set; } = null!;
    public bool IsActive { get; set; }
    public List<Order> Orders { get; set; } = new();


    public override string ToString()
    {
        return $"Id : {Id}, Fullname : {Fullname}, Email : {Email}, Address : {Address}, IsActive : {IsActive}";
    }
}
