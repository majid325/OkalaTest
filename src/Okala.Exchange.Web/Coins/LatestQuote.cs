using Okala.Exchange.UseCases;
using Okala.Exchange.UseCases.Contributors.List;

namespace Okala.Exchange.Web.Coins;



/// <summary>
/// Get a Coin with Latest Quote.
/// </summary>
/// <remarks>
/// Takes a symbol string  and returns a symbol with latest quote list.
/// </remarks>
public class GetBySymbol(IMediator _mediator)
  : Endpoint<GetLatestQuoteRequest, CoinDTO>
{
  public override void Configure()
  {
    Get(GetLatestQuoteRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetLatestQuoteRequest request,
    CancellationToken cancellationToken)
  {
    try
    {
      var query = new LatestQuoteQuery(request.Symbol);

      var result = await _mediator.Send(query, cancellationToken);

      if (result.Status == ResultStatus.NotFound)
      {
        await SendNotFoundAsync(cancellationToken);
        return;
      }

      if (result.IsSuccess)
      {
        Response = new CoinDTO(result.Value.Symbol, result.Value.Quotes);
      }
    }
    catch (Exception )
    {
      await SendErrorsAsync();
    }
  }
}
