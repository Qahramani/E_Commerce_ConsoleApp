using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Repositories.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUserRepository _userRepository;
    public OrderService()
    {
        _ordersRepository = new OrderRepository();
        _userRepository = new UserRepository();
    }
    public async Task CreateAsync(OrderPostDto orderPostDto)
    {
        var isUserExist = await _userRepository.IsExistAsync(x => x.Id == orderPostDto.UserId);
        if (!isUserExist)
            throw new NotFoundException("User of the order is not found");

        Order order = new()
        {
            UserId = orderPostDto.UserId,
            TotalAmount = orderPostDto.TotalAmount,
            Status = orderPostDto.Status,
            OrderDate = DateTime.UtcNow,
        };

        await _ordersRepository.CreateAsync(order);
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _getByIdAsync(id);
        _ordersRepository.Delete(order);
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task UpdateAsync(OrderPutDto orderPutDto)
    {
        var order = await _getByIdAsync(orderPutDto.Id);

        var isUserExist = await _userRepository.IsExistAsync(x => x.Id == orderPutDto.UserId);
        if (!isUserExist)
            throw new NotFoundException("User of the order is not found");

        order.Status = orderPutDto.Status;
        order.OrderDate = DateTime.UtcNow;
        order.TotalAmount = orderPutDto.TotalAmount;
        order.UserId = orderPutDto.UserId;

        _ordersRepository.Update(order);
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task<List<OrderGetDto>> GetAllAsync()
    {
        var orders = await _ordersRepository.GetAllAsync("OrderDetails","Payments");
        var ordersList = new List<OrderGetDto>();

        ordersList.ForEach(order =>
        {
            OrderGetDto orderGetDto = new()
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                OrderDate =order.OrderDate,
                TotalAmount = order.TotalAmount,
                Payments = order.Payments,
                OrderDetails = order.OrderDetails
            };
            ordersList.Add(orderGetDto);
        });

        return ordersList;
    }

    public async Task<OrderGetDto> GetByAsync(int id)
    {
        var order = await _getByIdAsync(id);
        OrderGetDto orderGetDto = new()
        {
            Id = order.Id,
            UserId = order.UserId,
            Status = order.Status,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Payments = order.Payments,
            OrderDetails = order.OrderDetails
        };
        return orderGetDto;
    }


    private async Task<Order> _getByIdAsync(int id)
    {
        var order = await _ordersRepository.GetSingleAsync(x => x.Id == id);
        if (order is null)
            throw new NotFoundException("Order is not found");

        return order;
    }
}
