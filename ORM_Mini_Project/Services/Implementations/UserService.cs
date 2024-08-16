using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.PaymentDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;
using ORM_Mini_Project.Utilities;

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
        var user = await getByIdAsync(id);
        _userRepository.Delete(user);
        await _userRepository.SaveAllChangesAsync();
    }

    public async Task UpdateUserInfoAsync(UserPutDto newUserDto)
    {
        var user = await getByIdAsync(newUserDto.Id);

        var isEmailExist = await _userRepository.IsExistAsync(x => x.Email == newUserDto.Email && x.Id != newUserDto.Id);
        if (isEmailExist)
            throw new AlreadyExistException("User with this email already exist");

        user.Fullname = newUserDto.Fullname;
        user.Password = newUserDto.Password;
        user.Address = newUserDto.Address;
        user.Email = newUserDto.Email;


        _userRepository.Update(user);
        await _userRepository.SaveAllChangesAsync();
        Colored.WriteLine("User updated succesfully ", ConsoleColor.DarkGreen);
    }

    public async Task<List<OrderGetDto>> GetUserOrdersAsync(int userId )
    {
        await getByIdAsync(userId);
        var user = await _userRepository.GetSingleAsync(x => x.Id == userId, "Orders.OrderDetails.Product");

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

    public async Task<UserGetDto> LoginUserASync(string email, string password)
    {
        var foundUser = await _userRepository.GetSingleAsync(x => x.Password ==  password && x.Email.ToLower() == email.ToLower());
        if (foundUser is null)
            throw new InvalidUserInformationException("User is not found");
        if (foundUser.IsActive is false)
            throw new UserIsBLokedException("Your account is blocked");

        UserGetDto loggedUser = new()
        {
            Id = foundUser.Id,
            Fullname = foundUser.Fullname,
            Email = foundUser.Email,
            Address = foundUser.Address,
        };
        return loggedUser;
    }

    public Task ExportUserPaymentsToExcel(int userId)
    {
        throw new NotImplementedException();
    }

   

    public async Task<User> getByIdAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == id);
        if (user is null)
            throw new NotFoundException("User is not found");

        return user;
    }

    public async Task DisActivateAccountAsync(int userId)
    {
        var user = await getByIdAsync(userId);
        user.IsActive = false;
        _userRepository.Update(user);
        await _userRepository.SaveAllChangesAsync();
    }
}
