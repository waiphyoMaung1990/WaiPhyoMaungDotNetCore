using Microsoft.AspNetCore.Mvc;
using WaiPhyoMaungDontNetCore.MvcApp.Models;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            var model = new HighChartModel.PieChartModel()
            {
                Title = "Egg Yolk Composition",
                Subtitle = "Source:<a href='https://www.mdpi.com/2072-6643/11/3/684/htm' target='_default'>MDPI</a>",
                Categories = new string[] { "Water", "Fat", "Carbohydrates", "Protein", "Ash" },
                Data = new double[] { 55.02, 26.71, 1.09, 15.5, 1.68 }
            };

            return View(model);
            
        }

        public IActionResult BasicArea()
        {
          //  var model = new HighChartModel.HumanBodyChart()
            {
                //Title = "Egg Yolk Composition",
                //Subtitle = "Source:<a href='https://www.mdpi.com/2072-6643/11/3/684/htm' target='_default'>MDPI</a>",
                //Categories = new string[] { "Water", "Fat", "Carbohydrates", "Protein", "Ash" },
                //Data = new double[] { 55.02, 26.71, 1.09, 15.5, 1.68 }
            };

            return View();

        }
    }
}
