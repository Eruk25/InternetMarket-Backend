using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Clients;
using InternetMarket.PaymentService.Application.Abstractions.PaymentGateway;
using InternetMarket.PaymentService.Application.Abstractions.UnitOfWork;
using InternetMarket.PaymentService.Application.DTOs;
using InternetMarket.PaymentService.Domain.Entities;
using InternetMarket.PaymentService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.PaymentService.Application.Transactions.Create
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
    {
        private readonly IOrderServiceClient _orderClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentGateway _paymentGateway;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IOrderServiceClient orderClient, IPaymentGateway paymentGateway)
        {
            _unitOfWork = unitOfWork;
            _orderClient = orderClient;
            _paymentGateway = paymentGateway;
        }

        public async Task<string> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var existingTransaction = await _unitOfWork.Transactions.GetByOrderIdAsync(request.OrderId);

            if (existingTransaction is not null && existingTransaction.CreatedAt > DateTime.UtcNow.AddMinutes(30))
                return _paymentGateway.BuildUrl(existingTransaction.ExternalToken);

            var order = await _orderClient.GetOrderByIdAsync(request.OrderId);
            var paymentData = await _paymentGateway.CreateSessionsAsync(order);
            var transaction = new Transaction(order.TotalPrice, paymentData.Token, PaymentConstants.CardId, request.OrderId);

            await _unitOfWork.Transactions.CreateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return paymentData.RedirectUrl;
        }
    }
}