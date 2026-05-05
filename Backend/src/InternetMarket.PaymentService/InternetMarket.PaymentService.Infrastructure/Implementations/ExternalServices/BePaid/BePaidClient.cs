using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.PaymentGateway;
using InternetMarket.PaymentService.Application.DTOs;
using InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Response;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid
{
    public class BePaidClient : IPaymentGateway
    {
        private readonly HttpClient _httpClient;
        public BePaidClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string BuildUrl(string token)
        {
            return $"https://checkout.bepaid.by/widget/hpp.html?{token}";
        }

        public async Task<PaymentData> CreateSessionsAsync(OrderDto orderDto)
        {
            int shopId = 4225;
            string secretKey = "3834fbef1fe6ea024ef77f5c79ec7ff1ba710ea6241c08c2f341afda8af4c1c4";
            var basicAuth = Encoding.UTF8.GetBytes($"{shopId}:{secretKey}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(basicAuth));
            var bePaidRequest = new BePaidCheckoutRequest
            {
                Checkout = new CheckoutDetails
                {
                    Test = true,
                    Attempts = 3,
                    IFrame = true,
                    Order = new OrderDetails
                    {
                        Currency = "BYN",
                        Amount = (int)(orderDto.TotalPrice * 100),
                        Description = "Оплата заказа",
                        TrackingId = orderDto.OrderId.ToString(),
                        AdditionalData = new AdditionalData
                        {
                            Cart = new Cart
                            {
                                Positions = orderDto.OrderItems.Select(oi => new CartItem
                                {
                                    Name = oi.ProductName,
                                    Amount = (int)(oi.UnitPrice * 100),
                                    Quantity = oi.Quantity,
                                    Description = oi.ProductId.ToString(),
                                    NomenclatureCode = oi.ProductId.ToString()
                                }).ToList()
                            }
                        }
                    },
                    Settings = new Settings
                    {
                        ReturnUrl = "http://localhost:3000/",
                        SuccessUrl = "http://localhost:3000/",
                        DeclineUrl = "https://bepaid.by",
                        FailUrl = "https://bepaid.by",
                        CancelUrl = "https://bepaid.by",
                        NotificationUrl = "https://powwow-bloating-compactly.ngrok-free.dev/api/payments/webhook",
                        ButtonNextText = "Вернуться в магазин",
                        AutoPay = false,
                        Language = "ru",
                        CustomerFields = new CustomerFields
                        {
                            Visible = new List<string> { "first_name", "last_name" }
                        },
                        PaymentType = new PaymentType
                        {
                            Types = new List<string> { "credit_card" }
                        },
                        Customer = new Customer
                        {
                            FirstName = orderDto.FirstName,
                            LastName = orderDto.LastName,
                            Address = orderDto.Address,
                            Country = "Belarus",
                            City = orderDto.City
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync("checkouts", bePaidRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error occurred: {error}");
            }

            var data = await response.Content.ReadFromJsonAsync<BePaidResponse>();

            return new PaymentData
            {
                Token = data!.Checkout.Token,
                RedirectUrl = data.Checkout.RedirectUrl
            };
        }
    }
}