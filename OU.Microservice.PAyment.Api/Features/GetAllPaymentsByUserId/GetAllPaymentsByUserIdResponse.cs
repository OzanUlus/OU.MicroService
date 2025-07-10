using OU.Microservice.Payment.Api.Repositories;

namespace OU.Microservice.Payment.Api.Features.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
        Guid Id,
        string OrderCode,
        string Amount,
        DateTime Created,
        PaymentStatus Status);
}
