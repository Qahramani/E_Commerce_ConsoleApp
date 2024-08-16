namespace ORM_Mini_Project.DTOs.UserDtos;

public class UserUpdatingAccountDto
{
    public string Fullname { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } = null!;
    public string Address { get; set; } = null!;
}
