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

namespace Okala.Exchange.UnitTests.Core.Services.CryptocurrencyService;
public class GetQuoteServiceTest_HttpStatus_IsHandle
{
  public class CryptocurrencyService_http_Test
  {


    [Theory]
    [ClassData(typeof(HttpResponseDataGenerator))]

    public async Task httpExceptionError_isHandle(HttpResponseMessage response)
    {
      var handlerMock = new Mock<HttpMessageHandler>();


      handlerMock
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
          "SendAsync",
          ItExpr.IsAny<HttpRequestMessage>(),
          ItExpr.IsAny<CancellationToken>())
        .ReturnsAsync(response);


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

}

public class HttpResponseDataGenerator : IEnumerable<object[]>
{
  private readonly List<object[]> _data = new List<object[]>
    {
         new object[] {new HttpResponseMessage{StatusCode = HttpStatusCode.Unauthorized} },
         new object[] {new HttpResponseMessage{StatusCode = HttpStatusCode.Forbidden } },
         new object[] { new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound } },
         new object[] {new HttpResponseMessage{StatusCode = HttpStatusCode.InternalServerError} }
    };

  public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
