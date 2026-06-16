using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Transaction;
using InternetMarket.PaymentService.Application.Abstractions.UnitOfWork;
using InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response;
using InternetMarket.PaymentService.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace InternetMarket.PaymentService.Application.Transactions.Update
{
    public class UpdateTransactionStatusCommandHandler : IRequestHandler<UpdateTransactionStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateTransactionStatusCommandHandler(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(UpdateTransactionStatusCommand request, CancellationToken cancellationToken)
        {
            var notification = JsonSerializer.Deserialize<BePaidWebhookDto>(request.Json);

            if (notification is null)
                throw new ArgumentNullException("Уведомление от BePaid отсутствует");

            var transaction = await _unitOfWork.Transactions.GetByOrderIdAsync(Guid.Parse(notification.Transaction.TrakingId));

            if (transaction is null)
                throw new ArgumentNullException("Транзакция не найдена");
            transaction.ConfirmSuccess();

            await _publishEndpoint.Publish(new TransactionSuccessful(transaction.OrderId));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}