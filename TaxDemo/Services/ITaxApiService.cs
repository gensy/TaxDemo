using System;
using System.Threading.Tasks;
using Taxdemo.Models;

namespace TaxDemo.Services
{
    public interface ITaxApiService
    {
        Task<string> GetTaxsRatesByLocation(string zip, string countryCode = "US", string stateCode = "", string city = "", string street = "");
        Task<string> GetOrderRates(TaxRequest data);
    }
}
