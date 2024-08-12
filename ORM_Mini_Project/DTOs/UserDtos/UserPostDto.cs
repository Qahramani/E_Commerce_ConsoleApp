using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.DTOs.UserDtos;

public class UserPostDto
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
}
