using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.Core.CoinAggregate;
public class Coin : EntityBase, IAggregateRoot
{
  public string Symbol { get; private set; }
  private List<Quote> _quotes;
  public IReadOnlyList<Quote> Quotes => _quotes.AsReadOnly();



#pragma warning disable CS8618
  private Coin() { }
#pragma warning restore CS8618
  private Coin(string? symbol)
  {
    
    _quotes = new List<Quote>();
    Symbol = symbol ?? "BTC";
  }


  public static Coin Create(string? symbol) => new Coin(symbol);


  public void SetUSDPrice(decimal price)
  {
    Guard.Against.Negative(price, nameof(price));

    var exist = _quotes.FirstOrDefault(d => d.Currency == Currency.USD);
    if (exist is null)
      _quotes.Add(Quote.Create(Currency.USD, price));
    else
      exist.SetPrice(price);
  }

  public void SetQuotes(Currency from, List<CurrencyRate> currencyRates)
  {
    var usd = _quotes.FirstOrDefault(d => d.Currency == Currency.USD);

    Guard.Against.Null(usd, nameof(usd));

    var baseCurrency = currencyRates.FirstOrDefault(d => d.Currency == from);
    var usdRate = currencyRates.FirstOrDefault(d => d.Currency == Currency.USD);
    Guard.Against.Null(usd, nameof(usdRate));
    Guard.Against.Null(baseCurrency, nameof(baseCurrency));


    foreach (CurrencyRate currentRate in currencyRates)
    {
      var exist = _quotes.FirstOrDefault(d => d.Currency == currentRate.Currency);
      if (exist is null)
      {
        var price = usd.Price * ((baseCurrency == currentRate) ? usdRate?.Rate ?? 0 : currentRate.Rate);

        _quotes.Add(Quote.Create(currentRate.Currency, price));
      }
    }
  }
}
