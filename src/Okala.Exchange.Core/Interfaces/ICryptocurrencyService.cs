using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.Core.Interfaces;

public interface ICryptocurrencyService
{
  Task<decimal> GetQuote(string? symbol = "BTC");
}
