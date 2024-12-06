using Okala.Exchange.Core.CoinAggregate;

namespace Okala.Exchange.Core.Interfaces;

public interface IRateService
{
  public Task<List<CurrencyRate>> GetRates();
}
