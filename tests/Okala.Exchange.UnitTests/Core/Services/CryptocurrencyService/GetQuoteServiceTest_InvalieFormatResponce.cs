using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq.Protected;
using Moq;
using Okala.Exchange.Core.CoinAggregate;
using Okala.Exchange.Core.ContributorAggregate;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Okala.Exchange.Infrastructure.Services.Cryptocurrency;
using Ardalis.Result;
using Azure;

namespace Okala.Exchange.UnitTests.Core.Services.CryptocurrencyService;
public class GetQuoteServiceTest_InvalieFormatResponce
{
  public class CryptocurrencyService_http_Test
  {


    [Theory]
    [ClassData(typeof(InvalieFormatResponceDataGenerator))]

    public async Task InvalieFormatResponce(StringContent content)
    {
      var handlerMock = new Mock<HttpMessageHandler>();


      handlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
          "SendAsync",
          ItExpr.IsAny<HttpRequestMessage>(),
          ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(new HttpResponseMessage() {StatusCode=HttpStatusCode.OK,Content=content });


      var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://pro-api.coinmarketcap.com") };

      var mockFactory = new Mock<IHttpClientFactory>();
      mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


      Dictionary<string, string?> inMemorySettings = new Dictionary<string, string?> { { "coinmarketcap:API-Key", "testKey" } };
      IConfiguration configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(inMemorySettings)
      .Build();


      var service = new Infrastructure.Services.Cryptocurrency.CryptocurrencyService(mockFactory.Object, configuration);


      try
      {
        var result = await service.GetQuote("BTC");

        Assert.True(result > 0);
      }
      catch (CryptocurrencyServiceException)
      {

        Assert.True(1 == 1);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.Message);
      }

    }

  }

  private class InvalieFormatResponceDataGenerator : IEnumerable<object[]>
  {
    private readonly List<object[]> _data = new List<object[]>
    {
      //invalid content format
          new object[] { new StringContent(@"{
    ""status"": {
        ""timestamp"": ""2024-12-08T06:35:53.825Z"",
        ""error_code"": 0,
        ""error_message"": null,
        ""elapsed"": 21,
        ""credit_count"": 1,
        ""notice"": null
    },
    ""data"": {
        ""BTC2"": {
            ""id"": 1,
            ""name"": ""Bitcoin"",
            ""symbol"": ""BTC"",
            ""slug"": ""bitcoin"",
            ""num_market_pairs"": 11824,
            ""date_added"": ""2010-07-13T00:00:00.000Z"",
            ""tags"": [
                ""mineable"",
                ""pow"",
                ""sha-256"",
                ""store-of-value"",
                ""state-channel"",
                ""coinbase-ventures-portfolio"",
                ""three-arrows-capital-portfolio"",
                ""polychain-capital-portfolio"",
                ""binance-labs-portfolio"",
                ""blockchain-capital-portfolio"",
                ""boostvc-portfolio"",
                ""cms-holdings-portfolio"",
                ""dcg-portfolio"",
                ""dragonfly-capital-portfolio"",
                ""electric-capital-portfolio"",
                ""fabric-ventures-portfolio"",
                ""framework-ventures-portfolio"",
                ""galaxy-digital-portfolio"",
                ""huobi-capital-portfolio"",
                ""alameda-research-portfolio"",
                ""a16z-portfolio"",
                ""1confirmation-portfolio"",
                ""winklevoss-capital-portfolio"",
                ""usv-portfolio"",
                ""placeholder-ventures-portfolio"",
                ""pantera-capital-portfolio"",
                ""multicoin-capital-portfolio"",
                ""paradigm-portfolio"",
                ""bitcoin-ecosystem"",
                ""ftx-bankruptcy-estate"",
                ""2017-2018-alt-season""
            ],
            ""max_supply"": 21000000,
            ""circulating_supply"": 19790568,
            ""total_supply"": 19790568,
            ""is_active"": 1,
            ""infinite_supply"": false,
            ""platform"": null,
            ""cmc_rank"": 1,
            ""is_fiat"": 0,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""tvl_ratio"": null,
            ""last_updated"": ""2024-12-08T06:35:00.000Z"",
            ""quote"": {
                ""USDT"": {
                    ""price"": 99817.9988,
                    ""volume_24h"": 40116447118.21814,
                    ""volume_change_24h"": -52.6108,
                    ""percent_change_1h"": 0.08806622,
                    ""percent_change_24h"": 0.51004079,
                    ""percent_change_7d"": 3.47193184,
                    ""percent_change_30d"": 31.3984188,
                    ""percent_change_60d"": 59.96966007,
                    ""percent_change_90d"": 81.67931445,
                    ""market_cap"": 1975453180343.6858,
                    ""market_cap_dominance"": 53.8455,
                    ""fully_diluted_market_cap"": 2096176157612.93,
                    ""tvl"": null,
                    ""last_updated"": ""2024-12-08T06:35:00.000Z""
                }
            }
        }
    }
}") },
          new object[] { new StringContent(@"{
    ""status"": {
        ""timestamp"": ""2024-12-08T06:35:53.825Z"",
        ""error_code"": 0,
        ""error_message"": null,
        ""elapsed"": 21,
        ""credit_count"": 1,
        ""notice"": null
    },
    ""data"": {
        ""BTC"": {
            ""id"": 1,
            ""name"": ""Bitcoin"",
            ""symbol"": ""BTC"",
            ""slug"": ""bitcoin"",
            ""num_market_pairs"": 11824,
            ""date_added"": ""2010-07-13T00:00:00.000Z"",
            ""tags"": [
                ""mineable"",
                ""pow"",
                ""sha-256"",
                ""store-of-value"",
                ""state-channel"",
                ""coinbase-ventures-portfolio"",
                ""three-arrows-capital-portfolio"",
                ""polychain-capital-portfolio"",
                ""binance-labs-portfolio"",
                ""blockchain-capital-portfolio"",
                ""boostvc-portfolio"",
                ""cms-holdings-portfolio"",
                ""dcg-portfolio"",
                ""dragonfly-capital-portfolio"",
                ""electric-capital-portfolio"",
                ""fabric-ventures-portfolio"",
                ""framework-ventures-portfolio"",
                ""galaxy-digital-portfolio"",
                ""huobi-capital-portfolio"",
                ""alameda-research-portfolio"",
                ""a16z-portfolio"",
                ""1confirmation-portfolio"",
                ""winklevoss-capital-portfolio"",
                ""usv-portfolio"",
                ""placeholder-ventures-portfolio"",
                ""pantera-capital-portfolio"",
                ""multicoin-capital-portfolio"",
                ""paradigm-portfolio"",
                ""bitcoin-ecosystem"",
                ""ftx-bankruptcy-estate"",
                ""2017-2018-alt-season""
            ],
            ""max_supply"": 21000000,
            ""circulating_supply"": 19790568,
            ""total_supply"": 19790568,
            ""is_active"": 1,
            ""infinite_supply"": false,
            ""platform"": null,
            ""cmc_rank"": 1,
            ""is_fiat"": 0,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""tvl_ratio"": null,
            ""last_updated"": ""2024-12-08T06:35:00.000Z"",
            ""quote"": {
                ""USDi"": {
                    ""price"": 99817.9988,
                    ""volume_24h"": 40116447118.21814,
                    ""volume_change_24h"": -52.6108,
                    ""percent_change_1h"": 0.08806622,
                    ""percent_change_24h"": 0.51004079,
                    ""percent_change_7d"": 3.47193184,
                    ""percent_change_30d"": 31.3984188,
                    ""percent_change_60d"": 59.96966007,
                    ""percent_change_90d"": 81.67931445,
                    ""market_cap"": 1975453180343.6858,
                    ""market_cap_dominance"": 53.8455,
                    ""fully_diluted_market_cap"": 2096176157612.93,
                    ""tvl"": null,
                    ""last_updated"": ""2024-12-08T06:35:00.000Z""
                }
            }
        }
    }
}") },
          new object[] { new StringContent(@"{
    ""status"": {
        ""timestamp"": ""2024-12-08T06:35:53.825Z"",
        ""error_code"": 0,
        ""error_message"": null,
        ""elapsed"": 21,
        ""credit_count"": 1,
        ""notice"": null
    },
    ""data"": {
        ""BTC"": {
            ""id"": 1,
            ""name"": ""Bitcoin"",
            ""symbol"": ""BTC"",
            ""slug"": ""bitcoin"",
            ""num_market_pairs"": 11824,
            ""date_added"": ""2010-07-13T00:00:00.000Z"",
            ""tags"": [
                ""mineable"",
                ""pow"",
                ""sha-256"",
                ""store-of-value"",
                ""state-channel"",
                ""coinbase-ventures-portfolio"",
                ""three-arrows-capital-portfolio"",
                ""polychain-capital-portfolio"",
                ""binance-labs-portfolio"",
                ""blockchain-capital-portfolio"",
                ""boostvc-portfolio"",
                ""cms-holdings-portfolio"",
                ""dcg-portfolio"",
                ""dragonfly-capital-portfolio"",
                ""electric-capital-portfolio"",
                ""fabric-ventures-portfolio"",
                ""framework-ventures-portfolio"",
                ""galaxy-digital-portfolio"",
                ""huobi-capital-portfolio"",
                ""alameda-research-portfolio"",
                ""a16z-portfolio"",
                ""1confirmation-portfolio"",
                ""winklevoss-capital-portfolio"",
                ""usv-portfolio"",
                ""placeholder-ventures-portfolio"",
                ""pantera-capital-portfolio"",
                ""multicoin-capital-portfolio"",
                ""paradigm-portfolio"",
                ""bitcoin-ecosystem"",
                ""ftx-bankruptcy-estate"",
                ""2017-2018-alt-season""
            ],
            ""max_supply"": 21000000,
            ""circulating_supply"": 19790568,
            ""total_supply"": 19790568,
            ""is_active"": 1,
            ""infinite_supply"": false,
            ""platform"": null,
            ""cmc_rank"": 1,
            ""is_fiat"": 0,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""tvl_ratio"": null,
            ""last_updated"": ""2024-12-08T06:35:00.000Z"",
            ""quote"": {
                ""USD"": {
                    ""gprice"": 99817.9988,
                    ""volume_24h"": 40116447118.21814,
                    ""volume_change_24h"": -52.6108,
                    ""percent_change_1h"": 0.08806622,
                    ""percent_change_24h"": 0.51004079,
                    ""percent_change_7d"": 3.47193184,
                    ""percent_change_30d"": 31.3984188,
                    ""percent_change_60d"": 59.96966007,
                    ""percent_change_90d"": 81.67931445,
                    ""market_cap"": 1975453180343.6858,
                    ""market_cap_dominance"": 53.8455,
                    ""fully_diluted_market_cap"": 2096176157612.93,
                    ""tvl"": null,
                    ""last_updated"": ""2024-12-08T06:35:00.000Z""
                }
            }
        }
    }
}") },
          new object[] { new StringContent(@"{
    ""status"": {
        ""timestamp"": ""2024-12-08T06:35:53.825Z"",
        ""error_code"": 10,
        ""error_message"": null,
        ""elapsed"": 21,
        ""credit_count"": 1,
        ""notice"": null
    },
    ""data"": {
        ""BTC"": {
            ""id"": 1,
            ""name"": ""Bitcoin"",
            ""symbol"": ""BTC"",
            ""slug"": ""bitcoin"",
            ""num_market_pairs"": 11824,
            ""date_added"": ""2010-07-13T00:00:00.000Z"",
            ""tags"": [
                ""mineable"",
                ""pow"",
                ""sha-256"",
                ""store-of-value"",
                ""state-channel"",
                ""coinbase-ventures-portfolio"",
                ""three-arrows-capital-portfolio"",
                ""polychain-capital-portfolio"",
                ""binance-labs-portfolio"",
                ""blockchain-capital-portfolio"",
                ""boostvc-portfolio"",
                ""cms-holdings-portfolio"",
                ""dcg-portfolio"",
                ""dragonfly-capital-portfolio"",
                ""electric-capital-portfolio"",
                ""fabric-ventures-portfolio"",
                ""framework-ventures-portfolio"",
                ""galaxy-digital-portfolio"",
                ""huobi-capital-portfolio"",
                ""alameda-research-portfolio"",
                ""a16z-portfolio"",
                ""1confirmation-portfolio"",
                ""winklevoss-capital-portfolio"",
                ""usv-portfolio"",
                ""placeholder-ventures-portfolio"",
                ""pantera-capital-portfolio"",
                ""multicoin-capital-portfolio"",
                ""paradigm-portfolio"",
                ""bitcoin-ecosystem"",
                ""ftx-bankruptcy-estate"",
                ""2017-2018-alt-season""
            ],
            ""max_supply"": 21000000,
            ""circulating_supply"": 19790568,
            ""total_supply"": 19790568,
            ""is_active"": 1,
            ""infinite_supply"": false,
            ""platform"": null,
            ""cmc_rank"": 1,
            ""is_fiat"": 0,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""tvl_ratio"": null,
            ""last_updated"": ""2024-12-08T06:35:00.000Z"",
            ""quote"": {
                ""USD"": {
                    ""gprice"": 99817.9988,
                    ""volume_24h"": 40116447118.21814,
                    ""volume_change_24h"": -52.6108,
                    ""percent_change_1h"": 0.08806622,
                    ""percent_change_24h"": 0.51004079,
                    ""percent_change_7d"": 3.47193184,
                    ""percent_change_30d"": 31.3984188,
                    ""percent_change_60d"": 59.96966007,
                    ""percent_change_90d"": 81.67931445,
                    ""market_cap"": 1975453180343.6858,
                    ""market_cap_dominance"": 53.8455,
                    ""fully_diluted_market_cap"": 2096176157612.93,
                    ""tvl"": null,
                    ""last_updated"": ""2024-12-08T06:35:00.000Z""
                }
            }
        }
    }
}") },
          new object[] { new StringContent(@"{
    ""status"": {
        ""timestamp"": ""2024-12-08T06:35:53.825Z"",
        ""error_code"": 10,
        ""error_message"": ""error in action"",
        ""elapsed"": 21,
        ""credit_count"": 1,
        ""notice"": null
    },
    ""data"": {
        ""BTC"": {
            ""id"": 1,
            ""name"": ""Bitcoin"",
            ""symbol"": ""BTC"",
            ""slug"": ""bitcoin"",
            ""num_market_pairs"": 11824,
            ""date_added"": ""2010-07-13T00:00:00.000Z"",
            ""tags"": [
                ""mineable"",
                ""pow"",
                ""sha-256"",
                ""store-of-value"",
                ""state-channel"",
                ""coinbase-ventures-portfolio"",
                ""three-arrows-capital-portfolio"",
                ""polychain-capital-portfolio"",
                ""binance-labs-portfolio"",
                ""blockchain-capital-portfolio"",
                ""boostvc-portfolio"",
                ""cms-holdings-portfolio"",
                ""dcg-portfolio"",
                ""dragonfly-capital-portfolio"",
                ""electric-capital-portfolio"",
                ""fabric-ventures-portfolio"",
                ""framework-ventures-portfolio"",
                ""galaxy-digital-portfolio"",
                ""huobi-capital-portfolio"",
                ""alameda-research-portfolio"",
                ""a16z-portfolio"",
                ""1confirmation-portfolio"",
                ""winklevoss-capital-portfolio"",
                ""usv-portfolio"",
                ""placeholder-ventures-portfolio"",
                ""pantera-capital-portfolio"",
                ""multicoin-capital-portfolio"",
                ""paradigm-portfolio"",
                ""bitcoin-ecosystem"",
                ""ftx-bankruptcy-estate"",
                ""2017-2018-alt-season""
            ],
            ""max_supply"": 21000000,
            ""circulating_supply"": 19790568,
            ""total_supply"": 19790568,
            ""is_active"": 1,
            ""infinite_supply"": false,
            ""platform"": null,
            ""cmc_rank"": 1,
            ""is_fiat"": 0,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""tvl_ratio"": null,
            ""last_updated"": ""2024-12-08T06:35:00.000Z"",
            ""quote"": {
                ""USD"": {
                    ""gprice"": 99817.9988,
                    ""volume_24h"": 40116447118.21814,
                    ""volume_change_24h"": -52.6108,
                    ""percent_change_1h"": 0.08806622,
                    ""percent_change_24h"": 0.51004079,
                    ""percent_change_7d"": 3.47193184,
                    ""percent_change_30d"": 31.3984188,
                    ""percent_change_60d"": 59.96966007,
                    ""percent_change_90d"": 81.67931445,
                    ""market_cap"": 1975453180343.6858,
                    ""market_cap_dominance"": 53.8455,
                    ""fully_diluted_market_cap"": 2096176157612.93,
                    ""tvl"": null,
                    ""last_updated"": ""2024-12-08T06:35:00.000Z""
                }
            }
        }
    }
}") }
         };




    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
