using DemoWIUTGallery.Interfaces;
using DemoWIUTGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWIUTGallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReadRange<Gallery> _readRange;
        public HomeController(ILogger<HomeController> logger, IReadRange<Gallery> readRange)
        {
            _logger = logger;
            _readRange = readRange;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _readRange.GetAllAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
