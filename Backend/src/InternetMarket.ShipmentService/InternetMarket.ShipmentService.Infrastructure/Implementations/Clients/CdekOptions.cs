namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients;

public class CdekOptions
{
    public string GrantType { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public int OriginCityCode { get; set; } = 9220;
    public string OriginCityName { get; set; } = "Минск";
    public string OriginAddress { get; set; } = "улица Немига, 46";
    public int DefaultCurrency { get; set; } = 7;
    public string CountryCodeFilter { get; set; } = "BY";
    public decimal FallbackTotalSum { get; set; } = 14.5m;
    public int FallbackPeriodMin { get; set; } = 3;
    public int FallbackPeriodMax { get; set; } = 5;
}
