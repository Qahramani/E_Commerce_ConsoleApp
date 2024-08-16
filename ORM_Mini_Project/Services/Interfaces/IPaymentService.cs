using ORM_Mini_Project.DTOs.PaymentDtos;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IPaymentService
{
    Task MakePaymentAsync(int orderId);
    Task<List<PaymentGetDto>> GetPaymentsAsync(int userId);
    Task<PaymentGetDto> GetPaymentByIdAsync(int paymentId);

}
