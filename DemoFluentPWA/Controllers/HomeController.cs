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
using Microsoft.Extensions.Configuration;
using WebPush;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace DemoFluentPWA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MongoItemRepo repository;
        private readonly MongoPushRepo pushRepository;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, MongoItemRepo itemRepo,
            MongoPushRepo pushRepo,
            IConfiguration config)
        {
            _logger = logger;
            repository = itemRepo;
            pushRepository = pushRepo;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PublicKey = _config.GetSection("VapidKeys")["PublicKey"];
            var items = await repository.GetAllAsync().ConfigureAwait(false);
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
            var subscribers = await pushRepository.GetAllAsync();

            string vapidPublicKey = _config.GetSection("VapidKeys")["PublicKey"];
            string vapidPrivateKey = _config.GetSection("VapidKeys")["PrivateKey"];
            PushPayloadModel payload = new PushPayloadModel { message = item.Description, title = "Nuovo articolo pubblicato" };
            foreach (var device in subscribers)
            {
                var pushSubscription = new PushSubscription(device.PushEndpoint, device.PushP256DH, device.PushAuth);
                var vapidDetails = new VapidDetails("mailto:s.natalini@outlook.com", vapidPublicKey, vapidPrivateKey);
                var webPushClient = new WebPushClient();
                webPushClient.SendNotification(pushSubscription, JsonConvert.SerializeObject(payload), vapidDetails);
            }
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
