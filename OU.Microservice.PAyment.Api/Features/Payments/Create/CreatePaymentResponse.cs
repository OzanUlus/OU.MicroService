﻿namespace OU.Microservice.Payment.Api.Features.Payments.Create
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}
