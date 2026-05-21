using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.DTOs;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Get
{
    public record GetCitiesQuery(string Name) : IRequest<IEnumerable<ShipmentCityResponse>>;
}