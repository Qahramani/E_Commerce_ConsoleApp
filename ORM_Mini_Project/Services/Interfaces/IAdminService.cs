using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IAdminService
{
    Task BlockUserAsync(int userId);
    Task UnblockUserASync(int userId);
    Task PrintUserInfoAsync(int userId);
    Task PrintAllUsersAsync();


}
