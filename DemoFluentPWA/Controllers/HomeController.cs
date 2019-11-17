using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoFluentPWA.Models;
using DemoFluentPWA.ServiceModel;
using DemoFluentPWA.Data;
using DemoFluentPWA.ViewModel;

namespace DemoFluentPWA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MongoRepository<ItemModel> repository;

        public HomeController(ILogger<HomeController> logger, MongoConnectionSettings settings)
        {
            _logger = logger;
            repository = new MongoRepository<ItemModel>(settings);
        }

        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();
            return View(new IndexViewModel() { items = items });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewArticle(string id)
        {
            var model = await repository.FindByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemModel item)
        {
            await repository.InsertOneAsync(item);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
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
