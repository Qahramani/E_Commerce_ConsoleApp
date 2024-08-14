using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Services.Implementations;

public class OrderService  : IOrderService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    public OrderService()
    {
        _ordersRepository = new OrderRepository();
        _userRepository = new UserRepository();
        _orderDetailRepository = new OrderDetailRepository();

    }

    public async Task AddOrderDetail(OrderDetailPostDto orderDetail)
    {
        var isOrderExist = _ordersRepository.IsExistAsync(x => x.Id == orderDetail.OrderId);
        if (isOrderExist is null)
            throw new InvalidOrderException("Order with this id is not exist");
        var isProductExist = _ordersRepository.IsExistAsync(x => x.Id == orderDetail.ProductId);
        if (isProductExist is null)
            throw new InvalidProductException("Product with this id is not exist");
        if (orderDetail.PricePerItem < 0 || orderDetail.Quantity < 0)
            throw new InvalidOrderDetailException("Price and quantity should be bigger than 0");

        OrderDetail newOtderDetail = new()
        {
            OrderId = orderDetail.OrderId,
            ProductId = orderDetail.ProductId,
            Quantity = orderDetail.Quantity,
            PricePerItem = orderDetail.PricePerItem
        };
        await _orderDetailRepository.CreateAsync(newOtderDetail);
        await _orderDetailRepository.SaveAllChangesAsync();

    }

    public async Task CancelOrder(int orderId)
    {
        var order = await _getByIdAsync(orderId);
        if (order.Status == OrderStatus.Cancelled)
            throw new OrderAlreadyCancelledException("Order already cancelled");
        order.Status = OrderStatus.Cancelled;
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task CompleteOrder(int orderId)
    {
        var order = await _getByIdAsync(orderId);
        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCompletedException("Order already completed");
        order.Status = OrderStatus.Completed;
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task CreateAsync(OrderPostDto orderPostDto)
    {
        if (orderPostDto.TotalAmount < 0)
            throw new InvalidOrderException("Total prrice cannot be negative");

        await _getByIdAsync(orderPostDto.UserId);

        Order order = new()
        {
            OrderDate = DateTime.UtcNow,
            TotalAmount = orderPostDto.TotalAmount,
            UserId = orderPostDto.UserId,
            Status = OrderStatus.Pending
        };
        await _ordersRepository.CreateAsync(order);
        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int orderId)
    {
        await _getByIdAsync(orderId);

        var order = await _ordersRepository.GetSingleAsync(x => x.Id == orderId, "OrderDetails");

        List<OrderDetailGetDto> orderDetailsList = new List<OrderDetailGetDto>();
        foreach (var orderDetail in order.OrderDetails)
        {
            OrderDetailGetDto dto = new()
            {
                Id = orderDetail.Id,
                PricePerItem = orderDetail.PricePerItem,
                Product = orderDetail.Product,
                Order = orderDetail.Order,
                Quantity = orderDetail.Quantity
            };
            orderDetailsList.Add(dto);
        }
        return orderDetailsList;

    }

    public async Task<List<OrderGetDto>> GetOrders()
    {
        var orders = await _ordersRepository.GetAllAsync();
        List<OrderGetDto> ordersList = new List<OrderGetDto>();
        foreach (var order in orders)
        {
            OrderGetDto dto = new()
            {
                Id = order.Id,
                User = order.User,
                Status = order.Status,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
            };
            ordersList.Add(dto);
        }
        return ordersList;
    }

    private async Task<Order> _getByIdAsync(int id)
    {
        var order = await _ordersRepository.GetSingleAsync(x => x.Id == id);
        if (order is null)
            throw new NotFoundException("Order is not found");

        return order;
    }

    
}
