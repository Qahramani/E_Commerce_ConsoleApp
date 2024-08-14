using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Services.Implementations;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Utilities;
using ORM_Mini_Project.Exceptions;

namespace ORM_Mini_Project
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            UserService userService = new UserService();
            ProductService productService = new ProductService();
            OrderService orderService = new OrderService();
            PaymentService paymentService = new PaymentService();
            AdminService adminService = new AdminService();

        restartMainMenu:
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
                        goto restartMainMenu;
                    case "2":
                        Console.WriteLine("--- User Login ---");
                        Console.Write("Username : ");
                        string loginEmail = Console.ReadLine();
                        Console.Write("Password : ");
                        string loginPassword = Console.ReadLine();
                        await userService.LoginUserASync(loginEmail, loginPassword);
                        break;
                    case "3":
                        Console.WriteLine("--- Admin Login ---");
                        Console.Write("Username : ");
                        string adminLoginUsername = Console.ReadLine();
                        Console.Write("Password : ");
                        string adminloginPassword = Console.ReadLine();
                        if (adminLoginUsername == AdminService.adminName && adminloginPassword == AdminService.adminPassword)
                            await AdminMenu(productService, adminService);
                        else
                            Colored.WriteLine("Invalid username/password for admin", ConsoleColor.DarkRed);

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
            catch(UserIsBLokedException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }
            catch (Exception ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartMainMenu;
            }










            //Console.WriteLine("Hello {}");
            //Console.WriteLine("--- User Menu ---");
            //Console.Write("[1] Products Menu\n" +
            //    "[2] Orders Menu\n" +
            //    "[3] Payment Menu\n" +
            //    ">>> ");


        }

        private static async Task AdminMenu(ProductService productService, AdminService adminService)
        {

        restartAdminMenu:
            Console.WriteLine("--- Admin Menu ---");
            Console.Write("[1] See all users\n" +
                "[2] Get user by Id\n" +
                "[3] Block User\n" +
                "[4] UnBlock User\n" +
                "[5] Create Product\n" +
                "[6] Update Product\n" +
                "[7] Delete Product\n" +
                "[8] Get All Products\n" +
                "[0] Exit\n" +
                ">>> ");

            string opt = Console.ReadLine();
            try
            {
                switch (opt)
                {
                    case "1":
                        Console.WriteLine("--- Users List ---");
                        await adminService.PrintAllUsersAsync();
                        goto restartAdminMenu;
                    case "2":

                        Console.Write("User Id : ");
                        int findId = int.Parse(Console.ReadLine());
                        await adminService.PrintUserInfoAsync(findId);
                        goto restartAdminMenu;
                    case "3":
                        Console.Write("User Id : ");
                        int blockId = int.Parse(Console.ReadLine());
                        await adminService.BlockUserAsync(blockId);
                        Colored.WriteLine("User blocked successfully", ConsoleColor.DarkGreen);

                        goto restartAdminMenu;
                    case "4":

                        Console.WriteLine("User Id : ");
                        int unBlockId = int.Parse(Console.ReadLine());
                        await adminService.UnblockUserASync(unBlockId);
                        Colored.WriteLine("User unblocked successfully", ConsoleColor.DarkGreen);
                        goto restartAdminMenu;
                    case "5":
                        Console.WriteLine("--- Product Creation ---");
                        Console.Write("Product name : ");
                        string createName = Console.ReadLine();
                        Console.Write("Price : ");
                        decimal createPrice = decimal.Parse(Console.ReadLine());
                        Console.Write("Stock : ");
                        int createStock = int.Parse(Console.ReadLine());
                        Console.Write("Description : ");
                        string createDesc = Console.ReadLine();

                        ProductPostDto createProduct = new()
                        {
                            Name = createName,
                            Price = createPrice,
                            Stock = createStock,
                            Description = createDesc
                        };
                        await productService.CreateAsync(createProduct);
                        Colored.WriteLine("Product created successully", ConsoleColor.DarkGreen);
                        goto restartAdminMenu;
                    case "6":
                        Console.WriteLine("--- Update Product ---");
                        Console.Write("Id : ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Product name : ");
                        string updateName = Console.ReadLine();
                        Console.Write("Price : ");
                        decimal updatePrice = decimal.Parse(Console.ReadLine());
                        Console.Write("Stock : ");
                        int updateStock = int.Parse(Console.ReadLine());
                        Console.Write("Description : ");
                        string updateDesc = Console.ReadLine();

                        ProductPutDto updateProduct = new()
                        {
                            Id = updateId,
                            Name = updateName,
                            Price = updatePrice,
                            Stock = updateStock,
                            Description = updateDesc
                        };
                        await productService.UpdateAsync(updateProduct);
                        Colored.WriteLine("Product updated successully", ConsoleColor.DarkGreen);
                        goto restartAdminMenu;
                    case "7":

                        Console.Write("Id : ");
                        int deleteId = int.Parse(Console.ReadLine());
                        await productService.DeleteAsync(deleteId);
                        Colored.WriteLine("Product deleted successully", ConsoleColor.DarkGreen);

                        goto restartAdminMenu;
                    case "8":

                        Console.WriteLine("--- Products List ---");
                        var products = await productService.GetAllAsync();
                        foreach (var p in products)
                        {
                            Console.WriteLine($"Name : {p.Name}, Price : {p.Price}, Stock : {p.Stock}, Desciption : {p.Description}");
                        }

                        goto restartAdminMenu;
                    case "0":
                        return;
                    default:
                        Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
                        goto restartAdminMenu;
                }
            }
            catch (NotFoundException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartAdminMenu;
            }
            catch (AlreadyExistException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartAdminMenu;
            }
            catch (InvalidProductException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartAdminMenu;
            }
            catch (Exception ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
                goto restartAdminMenu;
            }
        }
    }
}