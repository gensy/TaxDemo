using System.Collections.Generic;
using System.Net.Http;
using Taxdemo.Models;
using TaxDemo.Controllers;
using TaxDemo.Services;
using Xunit;
using Moq;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;

namespace TaxDemo.Tests
{
    public class ServiceMockTest
    {
        private readonly HomeController _controller;
        private readonly ITaxApiService _service;

        public ServiceMockTest()
        {
            //Loading configs
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();

            //Mocking HttpClientFactory 
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpResponseMessage result = new HttpResponseMessage();

            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(result)
            .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(config["KeyVault:TaxjarApiBase"])
            };

            httpClient.DefaultRequestHeaders.Add("Token", $"token=\"{config["KeyVault:TaxjarApiKey"]}\"");

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("PublicTaxApi")).Returns(httpClient);

            _service = new TaxApiService(mockHttpClientFactory.Object);
            _controller = new HomeController(_service);
        }


        [Fact]
        public void GetTaxRatesBylocationTest ()
        {
            // Act
            var okResult = _controller.Index("90404", "US", "CA", "Santa Monica", "22");
            // Assert
            Assert.True(okResult.IsCompletedSuccessfully);
        }

        [Fact]
        public void GetTaxesByOrderTest()
        {
            // Act
            var okResult = _controller.GetOrderRates(new TaxRequest{
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                amount = 15,
                shipping = 1.5,
              
                line_items = new List<LineItemRequest>{
                    new LineItemRequest{
                      quantity = 1,
                      product_tax_code = "20010",
                      unit_price = 15,
                    }}
            });
            // Assert
            Assert.True(okResult.IsCompletedSuccessfully);
        }
    }
}