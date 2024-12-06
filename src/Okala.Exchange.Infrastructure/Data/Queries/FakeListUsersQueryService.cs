using Okala.Exchange.Core.UserAggrigate;

namespace Okala.Exchange.Infrastructure.Data.Queries;

public class FakeListUsersQueryService : IListUsersQueryService
{
  public Task<IEnumerable<User>> ListAsync()
  {
    IEnumerable<User> result =
        [User.Create("user1","Aa123"),
        User.Create("user2", "Aa123")];

    return Task.FromResult(result);
  }
}
