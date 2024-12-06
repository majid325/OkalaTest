
namespace Okala.Exchange.UseCases;


public record CoinDTO(string Symbol, Dictionary<string, decimal> Quotes);

