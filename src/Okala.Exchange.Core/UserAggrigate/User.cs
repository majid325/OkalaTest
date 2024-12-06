using Okala.Exchange.Core.CoinAggregate;

namespace Okala.Exchange.Core.UserAggrigate;
public class User : EntityBase, IAggregateRoot
{

  public string UserName { get; private set; }
  public string Pass { get; private set; }


#pragma warning disable CS8618
  private User() { }
#pragma warning restore CS8618
  private User( string userName, string pass)
  {
    Guard.Against.NullOrEmpty(userName, nameof(userName));
    Guard.Against.NullOrEmpty(pass, nameof(pass));

    UserName = userName;
    Pass = pass;
  }


  public static User Create(string userName, string pass) => new User(userName,pass);



}
