using Okala.Exchange.Core.CoinAggregate;
using Okala.Exchange.Core.ContributorAggregate;
using Okala.Exchange.Core.Interfaces;

namespace Okala.Exchange.UseCases.Coins.LatestQuote;

public class LatestQuoteHandler(ICryptocurrencyService _cryptocurrencyService, IRateService _rateService)
  : IQueryHandler<LatestQuoteQuery, Result<CoinDTO>>
{
  public async Task<Result<CoinDTO>> Handle(LatestQuoteQuery request, CancellationToken cancellationToken)
  {
    var coin = Coin.Create(request.Symbol??"BTC");

    var usdPrice = await _cryptocurrencyService.GetQuote(request.Symbol);

    coin.SetUSDPrice(usdPrice);

    var rates = await _rateService.GetRates();

    coin.SetQuotes(Currency.EUR, rates);

    return Result.Success(new CoinDTO(coin.Symbol,new Dictionary<string, decimal>( coin.Quotes.Select(q=>new KeyValuePair<string,decimal>(q.Currency.Name,q.Price)))));
  }
}
