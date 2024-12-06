using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Okala.Exchange.Core.CoinAggregate;
using Okala.Exchange.Core.ContributorAggregate;
using Okala.Exchange.Core.Interfaces;

namespace Okala.Exchange.Infrastructure.Services.Rate;
internal class RateService: IRateService
{
  private readonly HttpClient _httpClient;
  private readonly string _getRatePath = "/v1/latest?access_key=";
  private readonly string[] _symbols = { "USD", "EUR", "BRL", "GBP", "AUD" };
  private readonly IConfiguration _config;
  public RateService(IHttpClientFactory httpClientFactory,IConfiguration config)
  {
    _httpClient = httpClientFactory.CreateClient("exchangeratesapi");

    Guard.Against.NullOrEmpty(config["exchangeratesapi:API-Key"], "exchangeratesapi=>API-Key");
    _config = config;
  }

  public async  Task<List<CurrencyRate>> GetRates()
  {
    var result = await _httpClient.GetFromJsonAsync<RateResponce>($"{_getRatePath}{_config["exchangeratesapi:API-Key"]}&symbols={string.Join(',', _symbols)}");

    if (result is not null && result.success )
    {
      List<CurrencyRate> rates = new List<CurrencyRate>();
      foreach (var rate in result.rates)
      {
        Currency.TryFromName(rate.Key, out var currency);
        if (currency != null) {
          rates.Add(CurrencyRate.Create(currency, rate.Value));
        }
      }

      return rates;
    }

    throw new Exception( "خطا در ارسال درخواست به سرویس نرخ ارز");
  }

  private record  RateResponce(bool success,long timestamp,[property:JsonPropertyName("base")]string baseCurrency,DateOnly date, Dictionary<string,decimal> rates);

  private record RateError(string code,string message);



}
