using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.PaymentDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(UserPostDto userDto);
    Task<UserGetDto> LoginUserASync(string  username, string password);
    Task UpdateUserInfoAsync(UserPutDto newUserDto);
    Task DeleteAsync(int id);
    Task ExportUserOrdersToExcelAsync(int userId);
    Task<List<OrderGetDto>> GetUserOrdersAsync(int userId);
    Task DisActivateAccountAsync(int userId);    
    
}
