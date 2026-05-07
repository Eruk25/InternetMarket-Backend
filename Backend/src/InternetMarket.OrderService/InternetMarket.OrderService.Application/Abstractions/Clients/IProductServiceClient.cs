using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Application.Abstractions.Clients
{
    public interface IProductServiceClient
    {
        Task ReserveAsync(Dictionary<Guid, int> itemsToReserve);
        Task CancelReservationAsync(Dictionary<Guid, int> itemsToCancelReservation);
        Task ConfirmShipmentAsync(Dictionary<Guid, int> itemsToConfirmShipment);
    }
}