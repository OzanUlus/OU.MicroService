using Refit;

namespace OU.Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public interface IPaymentService
    {
        [Post("/api/payments")]
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest paymentRequest);
    }
}
