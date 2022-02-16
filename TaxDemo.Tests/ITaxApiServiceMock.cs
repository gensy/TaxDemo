using System.Threading.Tasks;
using Taxdemo.Models;
using TaxDemo.Services;

namespace TaxDemo.Tests
{
    internal class taxApiServiceMock : ITaxApiService
    {
        private readonly string _rateByLocationJson;
        private readonly string _rateByOrderJson;

        public taxApiServiceMock()
        {
            _rateByLocationJson = "{\"rate\":{\"city\":\"SANTA MONICA\",\"city_rate\":\"0.0\",\"combined_district_rate\":\"0.03\",\"combined_rate\":" +
                "\"0.1025\",\"country\":\"US\",\"country_rate\":\"0.0\",\"county\":\"LOS ANGELES\",\"county_rate\":\"0.01\",\"freight_taxable\":" +
                "false,\"state\":\"CA\",\"state_rate\":\"0.0625\",\"zip\":\"90404\"}}";

            _rateByOrderJson = "{\"tax\":{\"amount_to_collect\":1.09,\"breakdown\":{\"city_tax_collectable\":0,\"city_tax_rate\":0,\"city_taxable_amount\"" +
                    ":0,\"combined_tax_rate\":0.06625,\"county_tax_collectable\":0,\"county_tax_rate\":0,\"county_taxable_amount\":0,\"line_items\":" +
                    "[{\"city_amount\":0,\"city_tax_rate\":0,\"city_taxable_amount\":0,\"combined_tax_rate\":0.06625,\"county_amount\":0,\"county_tax_rate\"" +
                    ":0,\"county_taxable_amount\":0,\"id\":\"1\",\"special_district_amount\":0,\"special_district_taxable_amount\":0,\"special_tax_rate\":0," +
                    "\"state_amount\":0.99,\"state_sales_tax_rate\":0.06625,\"state_taxable_amount\":15,\"tax_collectable\":0.99,\"taxable_amount\":15}]," +
                    "\"shipping\":{\"city_amount\":0,\"city_tax_rate\":0,\"city_taxable_amount\":0,\"combined_tax_rate\":0.06625,\"county_amount\":0," +
                    "\"county_tax_rate\":0,\"county_taxable_amount\":0,\"special_district_amount\":0,\"special_tax_rate\":0,\"special_taxable_amount\":0," +
                    "\"state_amount\":0.1,\"state_sales_tax_rate\":0.06625,\"state_taxable_amount\":1.5,\"tax_collectable\":0.1,\"taxable_amount\":1.5}," +
                    "\"special_district_tax_collectable\":0,\"special_district_taxable_amount\":0,\"special_tax_rate\":0,\"state_tax_collectable\":1.09," +
                    "\"state_tax_rate\":0.06625,\"state_taxable_amount\":16.5,\"tax_collectable\":1.09,\"taxable_amount\":16.5},\"freight_taxable\":true," +
                    "\"has_nexus\":true,\"jurisdictions\":{\"city\":\"RAMSEY\",\"country\":\"US\",\"county\":\"BERGEN\",\"state\":\"NJ\"},\"order_total_amount" +
                    "\":16.5,\"rate\":0.06625,\"shipping\":1.5,\"tax_source\":\"destination\",\"taxable_amount\":16.5}}";

        }

        public Task<string> GetTaxsRatesByLocation(string zip, string countryCode = "US", string stateCode = "", string city = "", string street = "")
        {
            //No logic to test here, very simple call    
            return Task.FromResult(_rateByLocationJson);
        }

        public Task<string> GetOrderRates(TaxRequest data)
        {
            //No logic to test here, very simple call    
            return Task.FromResult(_rateByOrderJson);
        }

        
    }
}
