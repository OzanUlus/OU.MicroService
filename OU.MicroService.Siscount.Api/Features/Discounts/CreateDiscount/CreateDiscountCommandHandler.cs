using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using OU.MicroService.Discount.Api.Repositories;
using System.Net;

namespace OU.MicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await appDbContext.Discounts.AnyAsync(x => x.UserId.ToString() == request.UserId.ToString() && x.Code == request.Code, cancellationToken: cancellationToken);


            if (hasCodeForUser)
            {
                return ServiceResult.Error("Discount code already exists for this user", HttpStatusCode.BadRequest);
            }

            var discount = new Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Rate = request.Rate,
                Created = DateTime.Now,
                Expired = request.Expired,
                UserId = request.UserId
            };

            await appDbContext.Discounts.AddAsync(discount, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);  

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
