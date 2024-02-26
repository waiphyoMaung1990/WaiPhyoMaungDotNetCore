using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WaiPhyoMaungDotNetCore.NLog.Models;

namespace WaiPhyoMaungDotNetCore.NLog.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILoggerManager _loggerManager;

        public HomeController(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

     

        public IActionResult Index()
        {
            _loggerManager.LogInfo("Success NLog");
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
