using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.OrderDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IProductRepository _productRepository;
    private readonly PaymentService _paymentService;
    public OrderService()
    {
        _ordersRepository = new OrderRepository();
        _userRepository = new UserRepository();
        _orderDetailRepository = new OrderDetailRepository();
        _productRepository = new ProductRepository();
        _paymentService = new PaymentService();
    }

    public async Task AddOrderDetail(OrderDetailPostDto orderDetail)
    {
        var foundOrder = await _ordersRepository.GetSingleAsync(x => x.Id == orderDetail.OrderId);

        if (foundOrder is null)
            throw new InvalidOrderException("Order with this id is not exist");

        var foundProduct = await _productRepository.GetSingleAsync(x => x.Id == orderDetail.ProductId);

        if (foundProduct is null)
            throw new InvalidProductException("Product with this id is not exist");

        if (orderDetail.Quantity < 0)
            throw new InvalidOrderDetailException("Price and quantity should be bigger than 0");

        foundOrder.TotalAmount += foundProduct.Price * orderDetail.Quantity;

        OrderDetail newOrderDetail = new()
        {
            OrderId = orderDetail.OrderId,
            ProductId = orderDetail.ProductId,
            Quantity = orderDetail.Quantity,
            PricePerItem = foundProduct.Price
        };

        _ordersRepository.Update(foundOrder);
        await _ordersRepository.SaveAllChangesAsync();
        _productRepository.Update(foundProduct);

        await _orderDetailRepository.CreateAsync(newOrderDetail);
        await _orderDetailRepository.SaveAllChangesAsync();

    }

    public async Task CancelOrder(int orderId)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCancelledException("Order already completed");
        if (order.Status == OrderStatus.Cancelled)
            throw new OrderAlreadyCancelledException("Order already cancelled");
        order.Status = OrderStatus.Cancelled;

        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task CompleteOrder(int orderId)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order.Status == OrderStatus.Cancelled)
            throw new OrderAlreadyCancelledException("Order already cancelled");
        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCompletedException("Order already completed");


        order.Status = OrderStatus.Completed;
        await _paymentService.MakePaymentAsync(orderId);

        await _ordersRepository.SaveAllChangesAsync();
    }

    public async Task<int> CreateOrderAsync(int userId)
    {
        Order order = new()
        {
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0,
            UserId = userId,
            Status = OrderStatus.Pending
        };
        await _ordersRepository.CreateAsync(order);
        await _ordersRepository.SaveAllChangesAsync();
        return order.Id;
    }


    public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int orderId)
    {
        await GetOrderByIdAsync(orderId);

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

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        var order = await _ordersRepository.GetSingleAsync(x => x.Id == id);
        if (order is null)
            throw new NotFoundException("Order is not found");


        return order;
    }


}
