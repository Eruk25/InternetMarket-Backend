using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ShipmentService.Application.Shipments.Update.UpdateStatus
{
    public record ReceivedStatusCommand(Guid OrderId) : IRequest;
}