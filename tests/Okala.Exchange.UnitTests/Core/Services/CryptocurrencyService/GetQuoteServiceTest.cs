using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okala.Exchange.Core.CoinAggregate;
using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.UnitTests.Core.Services.CryptocurrencyService;
public class GetQuoteServiceTest
{



  [Theory]
  //[InlineData(-1)]
  [InlineData(0)]
  [InlineData(1.1)]
  [InlineData(1.123456789123456789123456789123456789)]
  public void Coin_SetUSDPrice(decimal price)
  {
    var newCoin = Coin.Create(null);

    newCoin.SetUSDPrice(price);

    var usd = newCoin.Quotes.FirstOrDefault(d => d.Currency == Currency.USD);

    Assert.True(usd is not null);
    Assert.True(usd.Price > -1);

  }
}
