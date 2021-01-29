using LOLChampSelector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using LOLChampSelector.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace LOLChampSelector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        List<ChampInfo> champions = new List<ChampInfo>();
        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44317/api/");
                //HTTP GET
                var responseTask = client.GetAsync("LOLChampInfos");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ChampInfo>>();
                    readTask.Wait();

                    champions = readTask.Result;
                    ViewBag.ChampionList = ToSelectList(champions);
                }
                else //web api sent error response 
                {
                    //log response status here..

                    champions = new List<ChampInfo>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            ViewBag.champdata = champions;
            return View();
        }

        [NonAction]
        public SelectList ToSelectList(List<ChampInfo> champs)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in champs)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.ID.ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
