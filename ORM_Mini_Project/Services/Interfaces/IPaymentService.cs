using ORM_Mini_Project.DTOs.PaymentDtos;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IPaymentService
{
    Task MakePayment(PaymentPostDto paymentPost);
    Task<List<PaymentGetDto>> GetPayments();
}
