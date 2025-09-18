namespace OU.Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentResponse(bool Status, Guid? PaymentId, string? ErrorMessage);
}
