using Okala.Exchange.Core.ContributorAggregate;
using Okala.Exchange.Core.UserAggrigate;

namespace Okala.Exchange.Infrastructure.Data.Config;

public class ContributorConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(p => p.UserName)
        .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
        .IsRequired();

    builder.Property(p => p.Pass)
    .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
    .IsRequired();

  }
}
