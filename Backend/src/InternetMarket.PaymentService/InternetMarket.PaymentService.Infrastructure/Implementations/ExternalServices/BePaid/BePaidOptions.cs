namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid;

public class BePaidOptions
{
    public int ShopId { get; set; }
    public string SecretKey { get; set; } = string.Empty;
    public string CheckoutWidgetUrl { get; set; } = "https://checkout.bepaid.by/widget/hpp.html";
    public string NotificationBaseUrl { get; set; } = string.Empty;
    public string Currency { get; set; } = "BYN";
    public string Description { get; set; } = "Оплата заказа";
    public string Language { get; set; } = "ru";
    public string Country { get; set; } = "Belarus";
    public bool Test { get; set; } = true;
    public int Attempts { get; set; } = 3;
    public bool IFrame { get; set; } = true;
    public string PaymentTypes { get; set; } = "credit_card";
}
