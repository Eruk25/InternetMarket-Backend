using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Update.ConfirmShipment
{
    public record ConfirmShipmentCommand(Dictionary<Guid, int> ItemsToReserve) : IRequest;
}