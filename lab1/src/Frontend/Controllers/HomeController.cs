using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
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
            string url = "http://localhost:5000/api/values";
            string id = Post(url, data).Result;
            return Ok(id);
        }

        static async Task<string> Post(String url, String data)
        {
            HttpClient client = new HttpClient();
            var encoding = "application/x-www-form-urlencoded";
            var response = await client.PostAsync(url, new StringContent("=" + data, Encoding.UTF8, encoding));
            var id = await response.Content.ReadAsStringAsync();
            return id;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
