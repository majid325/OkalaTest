using Okala.Exchange.Core.CoinAggregate;

namespace Okala.Exchange.UnitTests.Core.ContributorAggregate;

public class CoinConstructor
{
  private readonly string _testSymbol = "BTC";
  private Coin? _testCoin;

  private Coin CreateContributor()
  {
    return  Coin.Create(_testSymbol);
  }

  [Fact]
  public void InitializesName()
  {
    _testCoin = CreateContributor();

    Assert.Equal(_testSymbol, _testCoin.Symbol);
  }
}
