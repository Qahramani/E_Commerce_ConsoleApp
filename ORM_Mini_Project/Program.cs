using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Services.Implementations;
using ORM_Mini_Project.Enums;

namespace ORM_Mini_Project
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            UserService userService = new UserService();
            //UserPostDto user = new()
            //{
            //    Fullname = "Fatima Velityev",
            //    Email = "faton@",
            //    Password = "salam123",
            //    Address = "Baku"
            //};


            //await userService.CreateAsync(user);

            //var users = await userService.GetAllAsync();

            //UserPutDto updateUser = new()
            //{
            //    Id = 6,
            //    Fullname = "Fatima Veliyeva",
            //    Password = "Hello123",
            //    Address = "Cexiya",
            //    Email = "faton@"
            //};
            //await userService.UpdateAsync(updateUser);


            //await userService.DeleteAsync(5); 

            //var user1 =await  userService.GetByIdAsync(2);
            //Console.WriteLine(user1.Fullname);


            ProductService productService = new ProductService();

            //ProductPostDto product = new()
            //{ 
            //    Name = "Computer",
            //    Price = 1200,
            //    Stock = 1,
            //};

            //await productService.CreateAsync(product);

            //ProductPutDto productPutDto = new()
            //{
            //    Id = 4,
            //    Name = "Phone(Changed)"
            //};

            //productService.UpdateAsync(productPutDto);




            OrderService orderService = new OrderService();

            OrderPostDto orderPostDto = new()
            {
                UserId = 1,
                TotalAmount  =123.23m,
                Status = OrderStatus.Completed
            };
            orderService.CreateAsync(orderPostDto);
        }


    }
}