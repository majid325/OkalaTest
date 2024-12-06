using Okala.Exchange.Infrastructure;

namespace Okala.Exchange.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
  {
    services.AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatrConfigs();


    if (builder.Environment.IsDevelopment())
    {



    }
    else
    {
    }

    logger.LogInformation("{Project} services registered", "Mediatr");

    return services;
  }


}
