using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Services.Implementations;
using ORM_Mini_Project.Services.Interfaces;
using ORM_Mini_Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Mini_Project.Helpers;

public static  class MainMenuService
{
    static  UserService userService = new UserService();
    public static async Task UserRegistrationAsync()
    {
        Console.WriteLine("--- User Registration ---");
        Console.Write("Username : ");
        string fullname = Console.ReadLine().Trim();

        Console.Write("Email : ");
        string email = Console.ReadLine().Trim();
        Validations.IsEmailCorrect(email);

        Console.Write("Password : ");
        string password = Console.ReadLine().Trim();
        Validations.IsPasswordCorrect(password);
        Console.Write("Address : ");
        string address = Console.ReadLine().Trim();
        UserPostDto newUser = new()
        {
            Fullname = fullname,
            Email = email,
            Password = password,
            Address = address
        };
        await userService.RegisterUserAsync(newUser);
        Colored.WriteLine("User created successfully", ConsoleColor.DarkGreen);
    }

    public static async Task<UserGetDto> UserLoginAsync()
    {
        Console.WriteLine("--- User Login ---");
        Console.Write("Email : ");
        string loginEmail = Console.ReadLine();
        Console.Write("Password : ");
        string loginPassword = Console.ReadLine();
        var loggedUser = await userService.LoginUserASync(loginEmail, loginPassword);

        return loggedUser;
    }

    public static async Task AdminLoginasync()
    {
        Console.WriteLine("--- Admin Login ---");
        Console.Write("Username : ");
        string adminLoginUsername = Console.ReadLine();
        Console.Write("Password : ");
        string adminloginPassword = Console.ReadLine();
        if (adminLoginUsername == AdminService.adminName && adminloginPassword == AdminService.adminPassword)
            await AdminMenuService.AdminMenu();
        else
            Colored.WriteLine("Invalid username/password for admin", ConsoleColor.DarkRed);
    }
}
