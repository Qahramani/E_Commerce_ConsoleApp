using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService()
    {
        _userRepository = new UserRepository();
    }
    public async Task RegisterUserAsync(UserPostDto userDto)
    {
        bool isUserExist = await _userRepository.IsExistAsync(x => x.Email == userDto.Email);
        if (isUserExist)
            throw new AlreadyExistException("User with given email already exist");

        User user = new User
        {
            Email = userDto.Email,
            Fullname = userDto.Fullname,
            Password = userDto.Password,
            Address = userDto.Address
        };

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveAllChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _getByIdAsync(id);
        _userRepository.Delete(user);
        await _userRepository.SaveAllChangesAsync();
    }

    public async Task UpdateUserInfoAsync(UserPutDto newUserDto)
    {
        var user = await _getByIdAsync(newUserDto.Id);

        var isEmailExist = await _userRepository.IsExistAsync(x => x.Email == newUserDto.Email && x.Id != newUserDto.Id);
        if (isEmailExist)
            throw new AlreadyExistException("User with this email already exist");

        user.Fullname = newUserDto.Fullname;
        user.Password = newUserDto.Password;
        user.Address = newUserDto.Address;
        user.Email = newUserDto.Email;


        _userRepository.Update(user);
        await _userRepository.SaveAllChangesAsync();
    }


    public async Task<List<UserGetDto>> GetUserOrdersAsync()
    {
        var users = await _userRepository.GetAllAsync("Order.OrderDetail","Order.Payment");

        List<UserGetDto> usersList = new List<UserGetDto>();
        foreach (var user in users)
        {
            UserGetDto userDto = new UserGetDto
            {
                Address = user.Address,
                Email = user.Email,
                Fullname = user.Fullname
            };
            usersList.Add(userDto);
        }
        return usersList;

    }


    public async Task<bool> LoginUserASync(string email, string password)
    {
        var foundUser = await _userRepository.GetSingleAsync(x => x.Password ==  password && x.Email == email);
        if (foundUser is null)
            throw new InvalidUserInformationException("User is not found");
        if (foundUser.IsActive is false)
            throw new UserIsBLokedException("Your account is blocked");
        return true;
    }

    public Task ExportUserOrdersToExcel(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderGetDto>> GetUserOrdersAsync(int userId)
    {
        await _getByIdAsync(userId);
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId,"Orders.OrderDetails");

        List<OrderGetDto> ordersList = new();


        foreach (var order in user.Orders)
        {
            OrderGetDto o = new()
            {
                Id = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderDetails = order.OrderDetails,
                Payments = order.Payments
            };
            ordersList.Add(o);
        }
        return ordersList;
    }

    private async Task<User> _getByIdAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == id);
        if (user is null)
            throw new NotFoundException("User is not found");

        return user;
    }
}
