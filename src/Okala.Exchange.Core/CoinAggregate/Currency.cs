namespace Okala.Exchange.Core.ContributorAggregate;

public class Currency : SmartEnum<Currency>
{
  public static readonly Currency USD = new(nameof(USD), 1);
  public static readonly Currency EUR = new(nameof(EUR), 2);
  public static readonly Currency BRL = new(nameof(BRL), 3);
  public static readonly Currency GBP = new(nameof(GBP), 4);
  public static readonly Currency AUD = new(nameof(AUD), 5);

  protected Currency(string name, int value) : base(name, value) { }
}

