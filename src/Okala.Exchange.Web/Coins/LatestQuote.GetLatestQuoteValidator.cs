using FluentValidation;

namespace Okala.Exchange.Web.Coins;



public class GetLatestQuoteValidator : Validator<GetLatestQuoteRequest>
{
  public GetLatestQuoteValidator()
  {
    //RuleFor(x => x.Symbol).Length(0, 10);
  }
}
