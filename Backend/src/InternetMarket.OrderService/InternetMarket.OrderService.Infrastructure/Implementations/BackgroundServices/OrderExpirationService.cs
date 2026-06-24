using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternetMarket.OrderService.Infrastructure.Implementations.BackgroundServices
{
    public class OrderExpirationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OrderExpirationService> _logger;

        public OrderExpirationService(IServiceProvider serviceProvider, ILogger<OrderExpirationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var expiredOrders = await orderRepository.GetExpiredCardOrdersAsync();

                    var productClient = scope.ServiceProvider.GetRequiredService<IProductServiceClient>();

                    foreach (var order in expiredOrders)
                    {
                        order.Cancel();
                        _logger.LogInformation("Order {OrderId} cancelled due to payment deadline expiration", order.Id);

                        var itemsToCancel = new Dictionary<Guid, int>();
                        foreach (var item in order.OrderItems)
                        {
                            itemsToCancel.Add(item.ProductId, item.Quantity);
                        }
                        await productClient.CancelReservationAsync(itemsToCancel);
                    }

                    await unitOfWork.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing expired orders");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
