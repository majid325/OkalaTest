using System.Net.Http;
using System.Net.Http.Json;
using Okala.Exchange.Core.Interfaces;

namespace Okala.Exchange.Infrastructure.Services.Cryptocurrency;
public class CryptocurrencyService : ICryptocurrencyService
{
  private readonly HttpClient _httpClient;
  private readonly string _GetQuotePath = "/v1/cryptocurrency/quotes/latest?symbol=";
  private readonly IConfiguration _config;
  public CryptocurrencyService(IHttpClientFactory httpClientFactory, IConfiguration config)
  {
    _httpClient = httpClientFactory.CreateClient("coinmarketcap");
    Guard.Against.NullOrEmpty(config["coinmarketcap:API-Key"], "coinmarketcap=>API-Key");
    _config = config;
  }


  public async Task<decimal> GetQuote(string? symbol = "BTC")
  {

    try
    {
      //_httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "e78f515f-5b2d-4b67-9fd8-89ea61ac1c9f");

      var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,$"{_GetQuotePath}{symbol}")
      {
       Headers =
        {
          { "X-CMC_PRO_API_KEY", _config["coinmarketcap:API-Key"] },
        }
      };
    
      var result1 = await _httpClient.SendAsync(httpRequestMessage);

      var result = await _httpClient.GetFromJsonAsync<GetQuoteResponce>($"{_GetQuotePath}{symbol}");

      if (result is not null && result.status.error_code == 0)
      {
        return result.data.First().Value.quote.First().Value.price;
      }

    }
    catch (System.Net.Http.HttpRequestException hex)
    {
      if (hex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        throw new Exception("توکن سرویس اکسچنج نامعتبر است");


      throw;

    }

    throw new Exception("خطا در ارسال درخواست به سرویس اکسچنج");
  }

  private record GetQuoteResponce(GetQuoteResponce_Status status, Dictionary<string, GetQuoteResponce_data> data);
  private record GetQuoteResponce_Status(DateTime timestamp, int error_code, string? error_message, int elapsed, int credit_count);
  private record GetQuoteResponce_data(int id, string name, string symbol, int credit_count, Dictionary<string, GetQuoteResponce_data_quote> quote);
  private record GetQuoteResponce_data_quote(decimal price, DateTime last_updated);




}


