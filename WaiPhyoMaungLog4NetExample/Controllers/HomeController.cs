using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WaiPhyoMaungLog4NetExample.Models;

namespace WaiPhyoMaungLog4NetExample.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILog _logger;

        public HomeController(ILog logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Info("This is Log4Net");
            return View();
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
