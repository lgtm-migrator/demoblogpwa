using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoFluentPWA.Data;
using DemoFluentPWA.Models;
using DemoFluentPWA.ServiceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DemoFluentPWA.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MongoPushRepo repository;

        public DevicesController(
            MongoPushRepo pushRepo)
        {
            repository = pushRepo;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Devices device)
        {
            await repository.InsertOneAsync(device);
            return NoContent();
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await repository.DeleteOneAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}