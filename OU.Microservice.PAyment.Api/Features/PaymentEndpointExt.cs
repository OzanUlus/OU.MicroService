using Asp.Versioning.Builder;
using OU.Microservice.Payment.Api.Features.Payments.Create;
using OU.Microservice.Payment.Api.Features.Payments.GetAllPaymentsByUserId;
using OU.Microservice.Payment.Api.Features.Payments.GetStatus;

namespace OU.Microservice.Payment.Api.Features
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint().GetPaymentStatusGroupItemEndpoint();
        }
    }
}
