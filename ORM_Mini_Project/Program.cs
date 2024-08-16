using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Services.Implementations;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Utilities;
using ORM_Mini_Project.Exceptions;
using System.Globalization;
using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.PaymentDtos;
using ORM_Mini_Project.Helpers;
using ClosedXML.Excel;
using ORM_Mini_Project.Services.Interfaces;
using ORM_Mini_Project.Contexts;

namespace ORM_Mini_Project
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            AppDbContext context = new AppDbContext();
        restartMainMenu:
            await context.SaveChangesAsync();
            Console.WriteLine("----- Main Menu -----");
            Console.Write("[1] Register\n" +
                "[2] Login\n" +
                "[3] Admin Login\n" +
                "[0] Exit\n" +
                ">>> ");
            string opt = Console.ReadLine();
            try
            {
                switch (opt)
                {
                    case "1":
                        await MainMenuService.UserRegistrationAsync();
                        goto restartMainMenu;
                    case "2":
                        var loggedUser = await MainMenuService.UserLoginAsync();
                        await UserMenuService.MenuAsync(loggedUser);
                        goto restartMainMenu;
                    case "3":
                        await MainMenuService.AdminLoginasync();
                        goto restartMainMenu;
                    case "0":
                        Colored.WriteLine("GoodBye....", ConsoleColor.DarkYellow);
                        return;
                    default:
                        Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
                        goto restartMainMenu;
                }
            }
            catch (AlreadyExistException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (InvalidUserInformationException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (WrongEmailException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (WrongPasswordException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (UserIsBLokedException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (Exception ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
        }

       
        }



    }
