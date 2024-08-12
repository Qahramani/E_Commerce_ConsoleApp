using ORM_Mini_Project.DTOs.OrderDetailDtos;
using ORM_Mini_Project.DTOs.OrderDtos;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IOrderService
{
    Task CreateAsync(OrderPostDto orderPostDto);
    Task CancelOrder(int orderId);
    Task CompleteOrder(int orderId);
    Task<List<OrderGetDto>> GetOrders();
    Task AddOrderDetail(OrderDetailPutDto orderDetail);

    Task<OrderDetailGetDto> GetOrderDetailsByOrderId(int orderId);

}
