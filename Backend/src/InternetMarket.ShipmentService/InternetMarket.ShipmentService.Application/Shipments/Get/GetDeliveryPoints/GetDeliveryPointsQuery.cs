using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints
{
    public record GetDeliveryPointsQuery(int CityCode) : IRequest<IEnumerable<DeliveryPointResponse>?>;
}