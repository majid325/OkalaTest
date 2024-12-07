using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.UseCases.Coins.LatestQuote;

public record LatestQuoteQuery(string? Symbol="BTC") : IQuery<Result<CoinDTO>>;
