using System.Net.Http;
using System.Net.Http.Json;
using Okala.Exchange.Core.Interfaces;
using Org.BouncyCastle.Utilities.Encoders;

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
      else if (result is null)
      {
        throw new CryptocurrencyServiceException("داده دریافتی معتبر نمی باشد", new Exception("result of GetQuoteResponce is null"));
      }
      else
      { 
        throw new CryptocurrencyServiceException(result.status?.error_message?? "خطا در فراخوانی سرویس اکیچنج", new Exception("result of result.status?.error_message is null"));

      }

    }
    catch (System.Net.Http.HttpRequestException hex)
    {
      if (hex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        throw new CryptocurrencyServiceException("توکن سرویس اکسچنج نامعتبر است", hex);
      if (hex.StatusCode != System.Net.HttpStatusCode.OK)
        throw new CryptocurrencyServiceException("خطا در فراخوانی سرویس اکیچنج", hex);

      throw;

    }

  }

  private record GetQuoteResponce(GetQuoteResponce_Status status, Dictionary<string, GetQuoteResponce_data> data);
  private record GetQuoteResponce_Status(DateTime timestamp, int error_code, string? error_message, int elapsed, int credit_count);
  private record GetQuoteResponce_data(int id, string name, string symbol, int credit_count, Dictionary<string, GetQuoteResponce_data_quote> quote);
  private record GetQuoteResponce_data_quote(decimal price, DateTime last_updated);

  


}


public class CryptocurrencyServiceException : Exception
{
  public CryptocurrencyServiceException(string message,Exception innerException):base(message, innerException) { }
  
}

