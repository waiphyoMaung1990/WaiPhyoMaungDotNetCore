
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using static WaiPhyoMaungDontNetCore.MvcApp.Models.CanvasJsModel;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult PieChart()
        {
            var model = new CanvasJsModel.PieChart()
            {
                DataPoints = new double[] { 79.45, 7.31, 7.06, 4.91, 1.26 },
                Labels = new string[] { "Google", "Bing", "Baidu", "Yahoo", "Others" },
                Title = "Desktop Search Engine Market Share - 2016"
            };

            return View(model);
        }

        public IActionResult PyramidChart()
        {
            var model = new CanvasJsModel.PyramidChart()
            {
                Title = "Software Sales Conversion",
                DataPoints = new List<DataPoint>
            {
                new DataPoint { Y = 100, Label = "Website Visit" },
                new DataPoint { Y = 65, Label = "Download Page Visit" },
                new DataPoint { Y = 45, Label = "Downloaded" },
                new DataPoint { Y = 32, Label = "Interested To Buy" },
                new DataPoint { Y = 5, Label = "Purchased" }
            }
            };

            return View(model);
        }
    }
}
