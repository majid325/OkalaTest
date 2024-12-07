using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.Core.CoinAggregate;
public class Quote
{
  public Currency Currency { get; private set; }
  public decimal Price { get; private set; }

  public Quote(Currency currency, decimal price)
  {
    Currency = currency;
    Price = price;
  }


  public void SetPrice(decimal price)
  { 
    Price = price;
  }


#pragma warning disable CS8618 
  private Quote(){}
#pragma warning restore CS8618 

  internal static Quote Create (Currency currency, decimal price)=>new Quote(currency, price);
}
