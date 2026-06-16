using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.Contracts.Events.Shipment;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.Abstractions.Repositories;
using InternetMarket.ShipmentService.Application.Abstractions.UnitOfWork;
using InternetMarket.ShipmentService.Domain.Entities;
using InternetMarket.ShipmentService.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Create
{
    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, CreateOrderDeliveryResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentClient _shipmentClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateShipmentCommandHandler(IShipmentRepository shipmentRepository, IShipmentClient shipmentClient, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _shipmentRepository = shipmentRepository;
            _shipmentClient = shipmentClient;
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<CreateOrderDeliveryResponse> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            DeliveryType deliveryType = request.DeliveryType switch
            {
                _ when request.DeliveryType == DeliveryType.OrderPickupPoint.Value => DeliveryType.OrderPickupPoint,
                _ when request.DeliveryType == DeliveryType.CourierDelivery.Value => DeliveryType.CourierDelivery,
                _ => throw new ArgumentException($"Неизвестный тип доставки: {request.DeliveryType}")
            };
            PaymentMethod paymentMethod = PaymentMethod.FromName(request.PaymentMethod);
            var orderInfo = await _shipmentClient.CreateOrderAsync(
                paymentMethod,
                request.ToCityCode,
                request.DeliveryPointId,
                deliveryType,
                request.City,
                request.Address,
                request.FullName,
                request.NumberPhone,
                request.OrderItems);

            var shipmentAmount = await _shipmentClient.CalculateTariffAsync(request.ToCityCode, deliveryType, request.OrderItems);
            var fullName = request.FullName.Split(" ");
            var shipment = new Shipment(
                orderInfo.ShipmentOrderId,
                request.OrderId,
                Location.Create(request.City, request.Address),
                new FullName(fullName[0], fullName[1]),
                NumberPhone.Create(request.NumberPhone),
                deliveryType,
                paymentMethod,
                shipmentAmount!.TotalSum);
            await _shipmentRepository.CreateAsync(shipment);
            await _publishEndpoint.Publish(new ShipmentCreated(shipment.OrderId));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return orderInfo;
        }
    }
}