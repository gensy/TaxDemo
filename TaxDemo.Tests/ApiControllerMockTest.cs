using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Taxdemo.Models;
using TaxDemo.Controllers;
using TaxDemo.Services;
using Xunit;

namespace TaxDemo.Tests
{
    public class ApiControllerMockTest
    {
        private readonly HomeController _controller;
        private readonly ITaxApiService _service;

        public ApiControllerMockTest()
        {
            _service = new taxApiServiceMock();
            _controller = new HomeController(_service);
        }

        [Fact]
        public void GetTaxRatesBylocationTest ()
        {
            // Act
            var okResult = _controller.Index("90404", "US", "CA", "Santa Monica", "22");
            // Assert
            Assert.True(okResult.IsCompletedSuccessfully);
            Assert.NotNull(okResult.Result);
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
            Assert.NotNull(okResult.Result);
        }
    }
}