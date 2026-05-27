using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Shipment;
using InternetMarket.ShipmentService.Application.Abstractions.Repositories;
using InternetMarket.ShipmentService.Application.Abstractions.UnitOfWork;
using MassTransit;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Update.UpdateStatus
{
    public class ReceivedShipmentStatusCommandHandler : IRequestHandler<ReceivedStatusCommand>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;
        public ReceivedShipmentStatusCommandHandler(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _shipmentRepository = shipmentRepository;
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ReceivedStatusCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(request.OrderId);

            if (shipment is null)
                throw new ArgumentNullException("Shipment was not found.");

            shipment.Received();

            await _shipmentRepository.UpdateAsync(shipment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new ShipmentReceived(request.OrderId));
        }
    }
}