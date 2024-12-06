using Okala.Exchange.Core.ContributorAggregate;

namespace Okala.Exchange.UseCases.Contributors.List;

public record LatestQuoteQuery(string? Symbol="BTC") : IQuery<Result<CoinDTO>>;
