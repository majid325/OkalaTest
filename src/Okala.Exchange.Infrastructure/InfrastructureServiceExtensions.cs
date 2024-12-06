using Okala.Exchange.Core.Interfaces;
using Okala.Exchange.Infrastructure.Data;
using Okala.Exchange.Infrastructure.Services.Cryptocurrency;
using Okala.Exchange.Infrastructure.Services.Rate;


namespace Okala.Exchange.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("SqliteConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlite(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<ICryptocurrencyService, CryptocurrencyService>()
           .AddScoped<IRateService, RateService>();



    services.AddHttpClient("coinmarketcap", act =>
    {
      Guard.Against.NullOrEmpty(config["coinmarketcap:BaseURL"], "coinmarketcap=>BaseURL");
      Guard.Against.NullOrEmpty(config["coinmarketcap:API-Key"], "coinmarketcap=>API-Key");

      act.BaseAddress = new Uri(config["coinmarketcap:BaseURL"] ??"");
      act.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", config["coinmarketcap:API-Key"]);
    });


    services.AddHttpClient("exchangeratesapi", act =>
    {
      Guard.Against.NullOrEmpty(config["exchangeratesapi:BaseURL"], "exchangeratesapi=>BaseURL");
      Guard.Against.NullOrEmpty(config["exchangeratesapi:API-Key"], "exchangeratesapi=>API-Key");

      act.BaseAddress = new Uri(config["exchangeratesapi:BaseURL"] ?? "");
     
    });

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
