using Microsoft.AspNetCore.Mvc;
using Service_Discovery_Client.Models;
using System.Diagnostics;
using System.Net.Http;

namespace Service_Discovery_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Test()
        {

            var httpClient = _httpClientFactory.CreateClient("Service_Discovery_Server_Api");

            using HttpResponseMessage response = await httpClient.GetAsync("https://Service_Discovery_Server_Api/api/customer");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            ViewBag.Hung = jsonResponse.ToString();

            return View();
        }




        public async Task<IActionResult> Customer(int id)
        {

            HttpClient httpClient = new HttpClient();

            using HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7156/api/Customer/{id}?customerid=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            ViewBag.Hung = jsonResponse.ToString();

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

       

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
