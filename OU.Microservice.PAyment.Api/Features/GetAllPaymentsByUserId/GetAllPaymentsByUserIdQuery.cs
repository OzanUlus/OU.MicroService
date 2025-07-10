using OU.Microservice.Shared;

namespace OU.Microservice.Payment.Api.Features.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
