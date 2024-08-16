using Microsoft.Extensions.Logging.Abstractions;
using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Services.Implementations;
using ORM_Mini_Project.Utilities;

namespace ORM_Mini_Project.Helpers
{
    public static class UserMenuService
    {
        static UserService userService = new UserService();
        static ProductService productService = new ProductService();
        static OrderService orderService = new OrderService();
        static PaymentService paymentService = new PaymentService();

        public static async Task MenuAsync(UserGetDto loggedUser)
        {

            Colored.WriteLine($"You're Welcome {loggedUser.Fullname} !", ConsoleColor.DarkMagenta);
        restartUserMenu:
            Console.WriteLine("--- User Menu ---");
            Console.Write("[1] Orders/Payments\n" +
                "[2] Update account info\n" +
                "[3] Delete Account\n" +
                "[0] Exit\n" +
                ">>> ");
            string opt = Console.ReadLine();
            try
            {
                switch (opt)
                {
                    case "1":
                        await OrdersMenuAsync(loggedUser);
                        goto restartUserMenu;

                    case "2":
                        var foundUser = await userService.getByIdAsync(loggedUser.Id);

                        Console.WriteLine("Your Current password : ");
                        string passwordChecking = Console.ReadLine();
                        if (passwordChecking != foundUser.Password)
                        {
                            Colored.WriteLine("Wrong password!", ConsoleColor.DarkRed);
                            goto restartUserMenu;
                        }
                        Console.Write("New Fullname : ");
                        string fullname = Console.ReadLine().Trim();

                        Console.Write("New Email : ");
                        string email = Console.ReadLine().Trim();
                        Validations.IsEmailCorrect(email);

                        Console.Write("New Password : ");
                        string password = Console.ReadLine().Trim();
                        Validations.IsPasswordCorrect(password);

                        Console.Write("New Address : ");
                        string address = Console.ReadLine().Trim();

                        UserPutDto updateDto = new()
                        {
                            Id = loggedUser.Id,
                            Fullname = fullname,
                            Email = email,
                            Password = password,
                            Address = address
                        };

                        await userService.UpdateUserInfoAsync(updateDto);
                        goto restartUserMenu;
                    case "3":
                        Colored.WriteLine("Are you sure that you want delete your accoun? Y/N", ConsoleColor.DarkYellow);
                        string answer = Console.ReadLine().Trim();
                        if (answer.ToUpper() == "N")
                            goto restartUserMenu;
                        else if (answer.ToUpper() == "Y")
                        {
                            Console.WriteLine("Please enter you password for security : ");
                            var deletePassword = Console.ReadLine().Trim();

                            if (deletePassword != loggedUser.Password)
                            {
                                Colored.WriteLine("Wrong password", ConsoleColor.DarkRed);
                                goto restartUserMenu;
                            }

                            await userService.DisActivateAccountAsync(loggedUser.Id);
                            return;
                        }
                        else
                            Colored.WriteLine("Invalid answer", ConsoleColor.DarkRed);

                        goto restartUserMenu;
                    case "0":
                        return;
                    default:
                        Colored.WriteLine("Invalid input", ConsoleColor.DarkRed);
                        goto restartUserMenu;
                }
            }
            catch (WrongEmailException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (WrongPasswordException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (AlreadyExistException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (NotFoundException ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            catch (Exception ex)
            {
                Colored.WriteLine($"{ex.Message}", ConsoleColor.DarkRed);
            }
            goto restartUserMenu;
        }

        public static async Task OrdersMenuAsync(UserGetDto loggedUser)
        {
        restartOrdersMenu:
            Console.WriteLine("--- Orders/Payments Menu ---");
            Console.Write("[1] Make Order\n" +
                "[2] Complete Order\n" +
                "[3] Cancel Order\n" +
                "[4] See your Orders\n" +
                "[5] See your Payments\n" +
                "[6] Export Payment to excel\n" +
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
                                Colored.Write(" | Out of stock", ConsoleColor.DarkYellow);
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
                        await PrintOrdersAsync(loggedUser);
                        goto restartOrdersMenu;
                    case "5":
                        Console.WriteLine("--- Payments List ---");
                        var paymentsGetDto = await paymentService.GetPaymentsAsync(loggedUser.Id);
                        foreach (var p in paymentsGetDto)
                        {
                            Console.WriteLine($"Order Id : {p.Order.Id}, Payment Date : {p.PaymentDate}, Amount : {p.Amount}");
                        }
                        goto restartOrdersMenu;
                    case "6":

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

        public static async Task PrintOrdersAsync(UserGetDto loggedUser)
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
    }
}
