using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Payment.Api.Repositories;
using OU.Microservice.Shared;

namespace OU.Microservice.Payment.Api.Features.Payments.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;
    public record GetPaymentStatusResponse(bool isPaid);


    public class GetPaymentStatusQueryHandler(AppDbContext context) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.OrderCode == request.orderCode, cancellationToken: cancellationToken);

            if (payment is null)
            {
                return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(false));
            }

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(payment.Status == PaymentStatus.Success));

        }
    }
}
