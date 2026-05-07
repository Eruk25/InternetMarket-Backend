using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.ProductService.Application.Products.Update.CancelReservation
{
    public record CancelReservationCommand(Dictionary<Guid, int> ItemsToReserve) : IRequest;
}