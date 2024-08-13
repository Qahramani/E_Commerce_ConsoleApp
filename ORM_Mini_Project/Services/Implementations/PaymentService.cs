using ORM_Mini_Project.DTOs.PaymentDtos;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrdersRepository _ordersRepository;
    public PaymentService()
    {
        _paymentRepository = new PaymentRepository();
        _ordersRepository = new OrderRepository();
    }

    public async Task<List<PaymentGetDto>> GetPayments()
    {
        var payments = await _paymentRepository.GetAllAsync();
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

    public async Task MakePayment(PaymentPostDto paymentPost)
    {
        if (paymentPost.Amount < 0)
            throw new InvalidPaymentException("Payment amount cannot be negative");
        var order = await _ordersRepository.GetSingleAsync(x => x.Id == paymentPost.OrderId);
        if (order is null)
            throw new NotFoundException("Order for this payment is not found");

        Payment payment = new()
        {
            OrderId = paymentPost.OrderId,
            Amount = paymentPost.Amount,
            PaymentDate = DateTime.UtcNow
        };
        await _paymentRepository.CreateAsync(payment);
        await _paymentRepository.SaveAllChangesAsync();
        
    }
}
