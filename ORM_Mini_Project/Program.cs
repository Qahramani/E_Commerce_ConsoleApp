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

            UserGetDto loggedUser = new UserGetDto();
        //restartMainMenu:
        //    Console.WriteLine("----- Main Menu -----");
        //    Console.Write("[1] Register\n" +
        //        "[2] Login\n" +
        //        "[3] Admin Login\n" +
        //        "[0] Exit\n" +
        //        ">>> ");
        //    string opt = Console.ReadLine();
        //    try
        //    {
        //        switch (opt)
        //        {
        //            case "1":
        //                Console.WriteLine("--- User Registration ---");
        //                Console.Write("Username : ");
        //                string fullname = Console.ReadLine().Trim();

        //                Console.Write("Email : ");
        //                string email = Console.ReadLine().Trim();
        //                Validations.IsEmailCorrect(email);

        //                Console.Write("Password : ");
        //                string password = Console.ReadLine().Trim();
        //                Validations.IsPasswordCorrect(password);
        //                Console.Write("Address : ");
        //                string address = Console.ReadLine().Trim();
        //                UserPostDto newUser = new()
        //                {
        //                    Fullname = fullname,
        //                    Email = email,
        //                    Password = password,
        //                    Address = address
        //                };
        //                await userService.RegisterUserAsync(newUser);
        //                Colored.WriteLine("User created successfully", ConsoleColor.DarkGreen);
        //                goto restartMainMenu;
        //            case "2":
        //                Console.WriteLine("--- User Login ---");
        //                Console.Write("Email : ");
        //                string loginEmail = Console.ReadLine();
        //                Console.Write("Password : ");
        //                string loginPassword = Console.ReadLine();
        //                loggedUser = await userService.LoginUserASync(loginEmail, loginPassword);
        //                break;
        //            case "3":
        //                Console.WriteLine("--- Admin Login ---");
        //                Console.Write("Username : ");
        //                string adminLoginUsername = Console.ReadLine();
        //                Console.Write("Password : ");
        //                string adminloginPassword = Console.ReadLine();
        //                if (adminLoginUsername == AdminService.adminName && adminloginPassword == AdminService.adminPassword)
        //                    await AdminMenu(productService, adminService);
        //                else
        //                    Colored.WriteLine("Invalid username/password for admin", ConsoleColor.DarkRed);

        //                goto restartMainMenu;
        //            case "0":
        //                Colored.WriteLine("GoodBye....", ConsoleColor.DarkYellow);
        //                return;
        //            default:
        //                Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
        //                goto restartMainMenu;
        //        }
        //    }
        //    catch (AlreadyExistException ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }
        //    catch (InvalidUserInformationException ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }
        //    catch (WrongEmailException ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }
        //    catch (WrongPasswordException ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }
        //    catch (UserIsBLokedException ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }
        //    catch (Exception ex)
        //    {
        //        Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
        //        goto restartMainMenu;
        //    }



            //    Colored.WriteLine($"You're Welcome {loggedUser.Fullname} !", ConsoleColor.DarkMagenta);
            //restartUserMenu:
            //Console.WriteLine("--- User Menu ---");
            //Console.Write("[1] Orders Menu\n" +
            //    "[2] Change \n" +
            //    "[3] Payments Menu\n" +
            //    "[] Settings\n" +
            //    "[0] Exit\n" +
            //    ">>> ");
            //string opt = Console.ReadLine();

            //switch (opt)
            //{
            //    case "1":
            //        restartOrdersMenu:
            //        Console.WriteLine("--- Orders Menu ---");
            //        Console.Write("[1] Make Order\n" +
            //            "[2] See your Orders\n" +
            //            ">>> ");
            //        opt = Console.ReadLine();
            //        switch (opt)
            //        {
            //            case "1":
            //                //Creating Order
            //                orderService.CreateAsync(loggedUser.Id);

            //                Console.Write("Product Name : ");
            //                string search = Console.ReadLine();
            //                var products =await  productService.GetAllAsync();
            //                foreach (ProductGetDto p in products)
            //                {
            //                    Console.WriteLine($"Id : {p.Id}, Name : {p.Name} , Price : {p.Price}, Description : {p.Description}");
            //                }
            //                Console.Write("Id of product that you want add to basket : ");
            //                int addId = int.Parse(Console.ReadLine());

            //                var foundProduct = await productService.GetByIdAsync(addId);

            //                Console.Write("Quantity : ");
            //                int quantity = int.Parse(Console.ReadLine());



            //                OrderDetailPostDto orderDto = new()
            //                {
            //                    OrderId = foundProduct.Id,
            //                    ProductId = foundProduct.Id,
            //                    PricePerItem = foundProduct.Price,
            //                    Quantity = quantity
            //                };
            //                await orderService.AddOrderDetail(orderDto);

            //                goto restartOrdersMenu;
            //            default:
            //                break;
            //        }
            //        goto restartUserMenu;
            //    default:
            //        Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
            //        goto restartUserMenu;
            //}


            loggedUser.Id = 1;
            await OrdersMenu(userService, productService, orderService, paymentService, loggedUser);
        }

        private static async Task OrdersMenu(UserService userService, ProductService productService, OrderService orderService, PaymentService paymentService, UserGetDto loggedUser)
        {
            

        restartOrdersMenu:
            Console.WriteLine("--- Orders Menu ---");
            Console.Write("[1] Make Order\n" +
                "[2] Complete Order\n" +
                "[3] Cancel Order\n" +
                "[4] See your Orders\n" +
                "[5] See your Payments\n" +
                "[0] Exit\n" +
                ">>> ");
            string opt = Console.ReadLine();
            var orders = await userService.GetUserOrdersAsync(loggedUser.Id);
            try
            {
                switch (opt)
                {
                    case "1":
                        int orderId;
                        Console.Write("Do you want create new order or update the exisiting one? N(New) / E(Existing) : ");
                        opt = Console.ReadLine();
                        if (opt.ToUpper() == "N")
                        {
                            orderId = await orderService.CreateOrderAsync(loggedUser.Id);
                        }
                        else if (opt.ToUpper() == "E")
                        {
                            Console.WriteLine("--- Pending Orders ---");
                            foreach (var order in orders)
                            {
                                if (order.Status == OrderStatus.Pending)
                                    Console.WriteLine($"Id : {order.Id}, TotalAmount : {order.TotalAmount}, OrderDate : {order.OrderDate}, Status : {order.Status}");
                            }
                            Console.Write("Id of order that you want update : ");
                            orderId = int.Parse(Console.ReadLine());
                            var foundOrder = await orderService.GetOrderByIdAsync(orderId);
                        }
                        else
                        {
                            Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
                            goto restartOrdersMenu;
                        }

                        //Console.Write("Product Name : ");
                        //string search = Console.ReadLine();
                        var products = await productService.GetAllAsync();
                        Console.WriteLine("--- Products List ---");
                        foreach (ProductGetDto p in products)
                        {
                            Console.Write($"\nId : {p.Id}, Name : {p.Name} , Price : {p.Price}, Description : {p.Description}");
                            if (p.Stock <= 0)
                            {
                                Colored.WriteLine(" | Out of stock", ConsoleColor.DarkYellow);
                            }
                        }
                        Console.Write("\nId of product that you want add to basket : ");
                        int addId = int.Parse(Console.ReadLine());

                        var foundProduct = await productService.GetByIdAsync(addId);

                        Console.Write("Quantity : ");
                        int quantity = int.Parse(Console.ReadLine());

                        OrderDetailPostDto orderDetailDto = new()
                        {
                            OrderId = orderId,
                            ProductId = foundProduct.Id,
                            PricePerItem = foundProduct.Price,
                            Quantity = quantity
                        };
                        await orderService.AddOrderDetail(orderDetailDto);
                        Colored.WriteLine("Order Detail added successfully", ConsoleColor.DarkGreen);

                        goto restartOrdersMenu;
                    case "2":
                        foreach (var order in orders)
                        {
                            if (order.Status == OrderStatus.Pending)
                                Console.WriteLine($"Id : {order.Id}, TotalAmount : {order.TotalAmount}, OrderDate : {order.OrderDate}");
                        }
                        Console.Write("Id of order that you want complete : ");
                        int completeId = int.Parse(Console.ReadLine());
                        await orderService.CompleteOrder(completeId);
                        Colored.WriteLine("Order was completed succsefully (Payment was made)", ConsoleColor.DarkGreen);
                        goto restartOrdersMenu;
                    case "3":
                        foreach (var order in orders)
                        {
                            if (order.Status == OrderStatus.Pending)
                                Console.WriteLine($"Id : {order.Id}, TotalAmount : {order.TotalAmount}, OrderDate : {order.OrderDate}");
                        }
                        Console.Write("Id of order that you want cancel : ");
                        int canceleId = int.Parse(Console.ReadLine());
                        await orderService.CancelOrder(canceleId);
                        Colored.WriteLine("Order was cancelled succsefully (Payment was made)", ConsoleColor.DarkGreen);
                        goto restartOrdersMenu;
                    case "4":
                        await PrintOrdersAsync(userService, loggedUser);
                        goto restartOrdersMenu;
                    case "5":
                        Console.WriteLine("--- Payments List ---");
                        var paymentsGetDto = await paymentService.GetPaymentsAsync(loggedUser.Id);
                        foreach (var p in paymentsGetDto)
                        {
                            Console.WriteLine($"Order Id : {p.Order.Id}, Payment Date : {p.PaymentDate}, Amount : {p.Amount}");
                        }

                        goto restartOrdersMenu;
                    case "0":
                        return;
                    default:
                        Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
                        goto restartOrdersMenu;
                }
            }
            catch (InvalidOrderException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (InvalidProductException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (InvalidOrderDetailException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (NotFoundException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (OrderAlreadyCancelledException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (OrderAlreadyCompletedException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (Exception ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            goto restartOrdersMenu;

        }

        private static async Task PrintOrdersAsync(UserService userService, UserGetDto loggedUser)
        {
            Console.WriteLine("--- Your Orders ---");
            var ordersList = await userService.GetUserOrdersAsync(loggedUser.Id);

            foreach (var order in ordersList)
            {
                Console.Write($"Id : {order.Id}, TotalAmount : {order.TotalAmount}, OrderDate : {order.OrderDate}, Status : ");
                if (order.Status == OrderStatus.Completed)
                    Colored.WriteLine("Completed", ConsoleColor.DarkGreen);
                else if (order.Status == OrderStatus.Pending)
                    Colored.WriteLine("Pending", ConsoleColor.DarkYellow);
                else if (order.Status == OrderStatus.Cancelled)
                    Colored.WriteLine("Canceled", ConsoleColor.DarkRed);
                foreach (var od in order.OrderDetails)
                {
                    Colored.WriteLine($"Product : {od.Product.Name},Quantity : {od.Quantity} ,PricePerItem : {od.PricePerItem}", ConsoleColor.White);
                }
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            }
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