﻿using MediatR;
using OU.Microservice.Order.Application.Contracts.Repositories;
using OU.Microservice.Order.Application.Contracts.UnitOfWork;
using OU.Microservice.Order.Domain.Entities;
using OU.Microservice.Shared;
using OU.Microservice.Shared.Services;
using System.Net;

namespace OU.Microservice.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IGenericRepository<int, Address> addressRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!request.Items.Any()) return ServiceResult.Error("Order items not found", "Order must have at least one item", HttpStatusCode.BadRequest);


            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };


            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);
            foreach (var orderItem in request.Items) order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);


            order.Address = newAddress;


            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            var paymentId = Guid.Empty;


            //Payment işlemleri yapılacak

            order.SetPaidStatus(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);


            return ServiceResult.SuccessAsNoContent();
        }
    }
}
