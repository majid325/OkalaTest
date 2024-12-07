using Okala.Exchange.Core.CoinAggregate;
using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.UnitTests.Core.CoinAggregate;
public class SetUSDPrice
{



  [Theory]
  //[InlineData(-1)]
  [InlineData(0)]
  [InlineData(1.1)]
  [InlineData(1.123456789123456789123456789123456789)]
  public void Coin_SetUSDPrice(decimal price)
  {
    var newCoin  = Coin.Create(null);

    newCoin.SetUSDPrice(price);

    var usd = newCoin.Quotes.FirstOrDefault(d => d.Currency == Currency.USD);

    Assert.True(usd is not null);
    Assert.True(usd.Price>-1);

  }
}
