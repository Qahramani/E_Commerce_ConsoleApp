using ORM_Mini_Project.DTOs.PaymentDtos;
using ORM_Mini_Project.Enums;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;
using ORM_Mini_Project.Utilities;

namespace ORM_Mini_Project.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductRepository _productRepository;
    public PaymentService()
    {
        _paymentRepository = new PaymentRepository();
        _ordersRepository = new OrderRepository();
        _productRepository = new ProductRepository();
    }

    public async Task<List<PaymentGetDto>> GetPaymentsAsync(int userId)
    {
        var payments = await _paymentRepository.GetFilterAsync(x => x.Id == userId, "Order");
        List<PaymentGetDto> paymentsList = new List<PaymentGetDto>();
        foreach (var payment in payments)
        {
            PaymentGetDto dto = new()
            {
                Order = payment.Order,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,
            };
            paymentsList.Add(dto);
        }
        return paymentsList;
    }

    public async Task MakePaymentAsync(int orderId)
    {
        var order = await GetOrderAsync(orderId);

        Payment payment = new()
        {
            OrderId = orderId,
            Amount = order.TotalAmount,
            PaymentDate = DateTime.UtcNow
        };

        order.Status = OrderStatus.Completed;

        decimal newStock;
        foreach (var orderDetail in order.OrderDetails)
        {
            newStock = (orderDetail.Product.Stock - orderDetail.Quantity);
            if (newStock < 0)
            {
                Colored.WriteLine($"We don't have enough products in stock, so we added only {orderDetail.Product.Stock} of product {orderDetail.Product.Name}, thanks!", ConsoleColor.DarkYellow);
                orderDetail.Quantity = orderDetail.Product.Stock;
                orderDetail.Product.Stock = 0;
            }
            else
            {
                orderDetail.Product.Stock -= orderDetail.Quantity;
            }

            _productRepository.Update(orderDetail.Product);
            await  _productRepository.SaveAllChangesAsync();
        }

        
        _ordersRepository.Update(order);

        await _paymentRepository.CreateAsync(payment);
        await _paymentRepository.SaveAllChangesAsync();
    }

    private async Task<Order> GetOrderAsync(int orderId)
    {
        var order = await _ordersRepository.GetSingleAsync(x => x.Id == orderId, "OrderDetails.Product");

        if (order is null)
            throw new NotFoundException("Order for this payment is not found");

        decimal totalAmount = 0;

        foreach (var od in order.OrderDetails)
        {
            if (od.Product.Stock < od.Quantity)
                od.Quantity = od.Product.Stock;


            totalAmount += od.PricePerItem * od.Quantity;
        }

        order.TotalAmount = totalAmount;

        return order;
    }
}
