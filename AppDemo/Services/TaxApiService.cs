using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Taxdemo.Models;

namespace TaxDemo.Services
{
    public class TaxApiService : ITaxApiService
    {
        private readonly HttpClient client;

        public TaxApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicTaxApi");
        }

        public async Task<string> GetTaxsRatesByLocation(string zip, string countryCode = "US", string stateCode = "", string city = "", string street = "")
        {
            var url = $"v2/rates/{zip}?country={countryCode.ToUpper()}&city={city}&street={street}";
            var response = await client.GetAsync(url);
            string stringResponse;
            if (response.IsSuccessStatusCode)
            {
                stringResponse = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return stringResponse;
        }

        public async Task<string> GetOrderRates(TaxRequest data)
        {
            var url = "v2/taxes";

            var json = JsonSerializer.Serialize(data);
            var dataJson = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, dataJson);

            string stringResponse;
            if (response.IsSuccessStatusCode)
            {
                stringResponse = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return stringResponse;
        }
    }
}
