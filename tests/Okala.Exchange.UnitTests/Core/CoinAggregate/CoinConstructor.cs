using Okala.Exchange.Core.CoinAggregate;

namespace Okala.Exchange.UnitTests.Core.ContributorAggregate;

public class CoinConstructor
{
  private Coin? _testCoin;



  [Theory]
  [InlineData(null)]
  [InlineData("btc")]
  [InlineData("BTC")]
  public void InitializesCoin(string? symbol)
  {
    _testCoin = Coin.Create(symbol);

    Assert.True(_testCoin is not null);
   
  }
}
