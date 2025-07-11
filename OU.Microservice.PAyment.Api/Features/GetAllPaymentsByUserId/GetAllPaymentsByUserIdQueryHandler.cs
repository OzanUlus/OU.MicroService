﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Payment.Api.Repositories;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;

namespace OU.Microservice.Payment.Api.Features.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService)
        : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(
            GetAllPaymentsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;

            var payments = await context.Payments
                .Where(x => x.UserId == userId)
                .Select(x => new GetAllPaymentsByUserIdResponse(
                    x.Id,
                    x.OrderCode,
                    x.Amount.ToString("C"), // Format as currency
                    x.Created,
                    x.Status))
                .ToListAsync(cancellationToken: cancellationToken);


            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
        }
    }
}
