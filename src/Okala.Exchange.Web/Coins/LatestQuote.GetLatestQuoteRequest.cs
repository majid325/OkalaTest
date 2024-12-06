namespace Okala.Exchange.Web.Coins;



public class GetLatestQuoteRequest
{
  public const string Route = "/Coin/{symbol}/LatestQuote";
  public static string BuildRoute(string? symbol) => Route.Replace("{symbol}", symbol);
  public string? Symbol { get; set; }
}
