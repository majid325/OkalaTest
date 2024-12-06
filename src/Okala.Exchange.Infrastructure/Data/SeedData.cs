using Okala.Exchange.Core.ContributorAggregate;
using Okala.Exchange.Core.UserAggrigate;

namespace Okala.Exchange.Infrastructure.Data;

public static class SeedData
{
  public static readonly User user1 = User.Create("user1", "Aa123");
  public static readonly User user2 = User.Create("user2", "Aa123");

  public static async Task InitializeAsync(AppDbContext dbContext)
  {
    if (await dbContext.Users.AnyAsync()) return; // DB has been seeded

    await PopulateTestDataAsync(dbContext);
  }

  public static async Task PopulateTestDataAsync(AppDbContext dbContext)
  {
    dbContext.Users.AddRange([user1, user2]);
    await dbContext.SaveChangesAsync();
  }
}
