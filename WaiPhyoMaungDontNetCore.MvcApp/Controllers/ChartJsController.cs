using Microsoft.AspNetCore.Mvc;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using static WaiPhyoMaungDontNetCore.MvcApp.Models.ChartJsModel;
using static WaiPhyoMaungDontNetCore.MvcApp.Models.HighChartModel;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class ChartJsController : Controller
    {
        #region ColumnChart

        public IActionResult ColumnChart()
        {
            var model = new ChartJsModel.ColumnChart()
            {
                Labels = new List<string> { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" },
                Data = new List<int> { 44, 55, 13, 43, 22, 49 },
            };
            return View(model);
        }

        #endregion

        #region LineChart
        public IActionResult LineChart()
        {
            var model = new ChartJsModel.LineChart()
            {
                Label = "My First Dataset",
                BorderColor = "rgb(75, 192, 192)",
                Data = new List<int> { 65, 59, 80, 81, 56, 55, 40 }
            };
            return View(model);
        }

        #endregion

        #region LineChart
        public IActionResult Doughnut()
        {
            var model = new ChartJsModel.Doughnut()
            {
                Label = "My First Dataset",
                BackgroundColor = new List<string> { "rgb(255, 99, 132)", "rgb(54, 162, 235)", "rgb(255, 205, 86)" },
                Data = new List<int> { 65, 59, 80, 81 }
            };

            return View(model);
        }
        #endregion

       
    }
}
