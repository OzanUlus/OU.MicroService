﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using OU.MicroService.Discount.Api.Repositories;
using System.Net;

namespace OU.MicroService.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);


            if (hasDiscount == null)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found", HttpStatusCode.NotFound);
            }

            if (hasDiscount.Expired < DateTime.Now)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount is expired", HttpStatusCode.BadRequest);
            }


            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(new GetDiscountByCodeQueryResponse(hasDiscount.Code, hasDiscount.Rate));
        }
    }
}
