using OU.Microservice.Shared;

namespace OU.Microservice.Payment.Api.Features.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
