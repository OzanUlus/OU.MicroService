namespace OU.Microservice.Order.Application.Contracts.Refit.PaymentService
{
    public record GetPaymentStatusResponse(Guid? PaymentId, bool isPaid)
    {
    }
}
