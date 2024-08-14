using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;
using ORM_Mini_Project.Utilities;

namespace ORM_Mini_Project.Services.Implementations;

public class AdminService : IAdminService
{

    public static  string adminPassword = "Admin123";
    public static string adminName = "Admin";

    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    public AdminService()
    {
        _userRepository = new UserRepository();
        _productRepository = new ProductRepository();
    }
    public async Task BlockUserAsync(int userId)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId);
        if (user is null)
            throw new NotFoundException("User is not found");
        user.IsActive = false;
        _userRepository.SaveAllChangesAsync();
    }
    public async Task UnblockUserASync(int userId)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId);
        if (user is null)
            throw new NotFoundException("User is not found");
        user.IsActive = true;
        _userRepository.SaveAllChangesAsync();
    }

    public async Task PrintAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users)
        {
            if (user.IsActive == false)
            {
                Console.Write(user);
                Colored.WriteLine("  | BLOCKED |", ConsoleColor.DarkRed);
                continue;
            }
            Console.WriteLine(user);
        }
    }

    public async Task PrintUserInfoAsync(int userId)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId,"Orders");
        if (user is null)
            throw new NotFoundException("User is not found");

        Console.WriteLine(user);
        Console.WriteLine("Orders : ");
        foreach (var order in user.Orders)
        {
            Console.WriteLine(order);
        }

    }

  
}
