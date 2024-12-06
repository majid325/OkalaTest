namespace Okala.Exchange.Core.UserAggrigate;
public interface IListUsersQueryService
{
  Task<IEnumerable<User>> ListAsync();
}
