using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints
{
    public class GetDeliveryPointsQueryHandler : IRequestHandler<GetDeliveryPointsQuery, IEnumerable<DeliveryPointResponse>?>
    {
        private readonly IShipmentClient _shipmentClient;

        public GetDeliveryPointsQueryHandler(IShipmentClient shipmentClient)
        {
            _shipmentClient = shipmentClient;
        }

        public async Task<IEnumerable<DeliveryPointResponse>?> Handle(GetDeliveryPointsQuery request, CancellationToken cancellationToken)
        {
            var deliveryPoints = await _shipmentClient.GetDeliveryPointsAsync(request.CityCode);
            if (deliveryPoints is null)
                return null;

            return deliveryPoints.Select(dp => new DeliveryPointResponse
            {
                DeliveryCode = dp.DeliveryCode,
                Location = new DeliveryLocation
                {
                    CityCode = dp.Location.CityCode,
                    City = dp.Location.City,
                    Address = dp.Location.Address
                }
            });
        }
    }
}