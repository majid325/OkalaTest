using Microsoft.AspNetCore.Mvc;
using FromHeaderAttribute = FastEndpoints.FromHeaderAttribute;

namespace Okala.Exchange.Web.Coins;



public class GetLatestQuoteRequest
{
  public const string Route = "/Coin/LatestQuote/";
  //public static string BuildRoute(string? symbol) => Route.Replace("", symbol);


  public string? Symbol { get; set; }
}
