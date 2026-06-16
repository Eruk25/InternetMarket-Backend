using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using InternetMarket.PaymentService.Application.Abstractions.PaymentGateway;
using InternetMarket.PaymentService.Application.DTOs;
using InternetMarket.PaymentService.Application.DTOs.PaymentGateway.Response;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Response;
using Microsoft.Extensions.Options;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid;

public class BePaidClient : IPaymentGateway
{
    private readonly HttpClient _httpClient;
    private readonly BePaidOptions _bePaidOptions;
    private readonly FrontendOptions _frontendOptions;

    public BePaidClient(HttpClient httpClient, IOptions<BePaidOptions> bePaidOptions, IOptions<FrontendOptions> frontendOptions)
    {
        _httpClient = httpClient;
        _bePaidOptions = bePaidOptions.Value;
        _frontendOptions = frontendOptions.Value;
    }

    public string BuildUrl(string token)
    {
        return $"{_bePaidOptions.CheckoutWidgetUrl}?{token}";
    }

    public async Task<PaymentData> CreateSessionsAsync(OrderDto orderDto)
    {
        var basicAuth = Encoding.UTF8.GetBytes($"{_bePaidOptions.ShopId}:{_bePaidOptions.SecretKey}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(basicAuth));

        var bePaidRequest = new BePaidCheckoutRequest
        {
            Checkout = new CheckoutDetails
            {
                Test = _bePaidOptions.Test,
                Attempts = _bePaidOptions.Attempts,
                IFrame = _bePaidOptions.IFrame,
                Order = new OrderDetails
                {
                    Currency = _bePaidOptions.Currency,
                    Amount = (int)(orderDto.TotalPrice * 100),
                    Description = _bePaidOptions.Description,
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
                    ReturnUrl = $"{_frontendOptions.BaseUrl}/payment/return",
                    SuccessUrl = $"{_frontendOptions.BaseUrl}/payment/success",
                    DeclineUrl = $"{_frontendOptions.BaseUrl}/payment/decline",
                    FailUrl = $"{_frontendOptions.BaseUrl}/payment/fail",
                    CancelUrl = $"{_frontendOptions.BaseUrl}/payment/cancel",
                    NotificationUrl = $"{_bePaidOptions.NotificationBaseUrl}/api/payments/webhook",
                    ButtonNextText = "Вернуться в магазин",
                    AutoPay = false,
                    Language = _bePaidOptions.Language,
                    CustomerFields = new CustomerFields
                    {
                        Visible = new List<string> { "first_name", "last_name" }
                    },
                    PaymentType = new PaymentType
                    {
                        Types = _bePaidOptions.PaymentTypes.Split(',').Select(t => t.Trim()).ToList()
                    },
                    Customer = new Customer
                    {
                        FirstName = orderDto.FirstName,
                        LastName = orderDto.LastName,
                        Address = orderDto.Address,
                        Country = _bePaidOptions.Country,
                        City = orderDto.City
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("checkouts", bePaidRequest);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка при обработке платежа: {error}");
        }

        var data = await response.Content.ReadFromJsonAsync<BePaidResponse>();

        return new PaymentData
        {
            Token = data!.Checkout.Token,
            RedirectUrl = data.Checkout.RedirectUrl
        };
    }
}
