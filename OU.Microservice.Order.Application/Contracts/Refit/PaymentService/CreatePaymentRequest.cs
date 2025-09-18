using OU.Microservice.Shared;

namespace OU.Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentRequest(
        string OrderCode,
        string CardNumber,
        string CardHolderName,
        string CardExpirationDate,
        string CardSecurityNumber,
        decimal Amount);
}
