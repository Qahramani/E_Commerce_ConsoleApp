using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.UserDtos;

public class UserPostDto
{
    public string Fullname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Address { get; set; } = null!;
}
