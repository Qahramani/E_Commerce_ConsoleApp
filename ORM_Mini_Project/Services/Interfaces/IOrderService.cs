using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.OrderDtos;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(int userId);
    Task CancelOrder(int orderId);
    Task CompleteOrder(int orderId);
    Task<List<OrderGetDto>> GetOrders();
    Task AddOrderDetail(OrderDetailPostDto orderDetail);
    Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderId(int orderId);
}
