using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IUserService
{



    Task RegisterUserAsync(UserPostDto userDto);

    Task<bool> LoginUserASync(string  username, string password);
    Task UpdateUserInfoAsync(UserPutDto newUserDto);
    Task DeleteAsync(int id);
    Task ExportUserOrdersToExcel(int userId);
    Task<List<OrderGetDto>> GetUserOrdersAsync(int userId);
}
