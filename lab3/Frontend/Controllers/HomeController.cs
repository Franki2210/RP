using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        const string mediaType = "application/x-www-form-urlencoded";

        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(string data)
        {
            return Ok(!String.IsNullOrEmpty(data) ? GetString(data).Result : default(string));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> GetString(string value)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(configuration["backend.url"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                StringContent content = new StringContent($"={value}", Encoding.UTF8, mediaType);
                HttpResponseMessage response = await client.PostAsync("api/values", content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
