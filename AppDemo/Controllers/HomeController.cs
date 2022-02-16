using System.Threading.Tasks;
using TaxDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Taxdemo.Models;

namespace TaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaxApiService _taxApiService;

        public HomeController(ITaxApiService taxApiService)
        {
            _taxApiService = taxApiService;
        }

        // GET api/<HomeController>/5
        public async Task<IActionResult> Index(string zip, string countryCode, string stateCode, string city, string street)
        {
            string output = "";

            if (!string.IsNullOrEmpty(zip))
            {
                output = await _taxApiService.GetTaxsRatesByLocation(zip, countryCode, stateCode, city, street);
            }

            ViewData["DataResponse"] = output;

            return View();
        }

        // POST api/<HomeController>
        public async Task<IActionResult> GetOrderRates(TaxRequest data)
        {
            string output = "";

            if (data != null)
            {
                output = await _taxApiService.GetOrderRates(data);
            }

            ViewData["DataResponse"] = output;

            return View("Index");
        }

    }
}
