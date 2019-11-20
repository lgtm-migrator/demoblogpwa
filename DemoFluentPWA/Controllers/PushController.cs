using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoFluentPWA.Data;
using DemoFluentPWA.Models;
using DemoFluentPWA.ServiceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebPush;

namespace DemoFluentPWA.Controllers
{
    public class PushController : Controller
    {
        
        private readonly IConfiguration _configuration;

        public PushController(MongoConnectionSettings settings, IConfiguration config)
        {
            settings.CollectionName = "push";
            _configuration = config;
        }

        public IActionResult GenerateKeys()
        {
            var keys = VapidHelper.GenerateVapidKeys();
            ViewBag.PublicKey = keys.PublicKey;
            ViewBag.PrivateKey = keys.PrivateKey;
            return View();
        }
    }
}