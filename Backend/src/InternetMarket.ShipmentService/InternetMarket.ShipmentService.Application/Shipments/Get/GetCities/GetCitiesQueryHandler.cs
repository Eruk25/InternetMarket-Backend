using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.DTOs;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Get
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<ShipmentCityResponse>?>
    {
        private readonly IShipmentClient _shipmentClient;
        public GetCitiesQueryHandler(IShipmentClient shipmentClient)
        {
            _shipmentClient = shipmentClient;
        }

        public async Task<IEnumerable<ShipmentCityResponse>?> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _shipmentClient.GetCitiesAsync(request.Name);

            return cities;
        }
    }
}