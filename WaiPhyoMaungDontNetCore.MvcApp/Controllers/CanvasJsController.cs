﻿
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
        #region AreaChart
        public IActionResult AreaChart()
        {
            var model = new CanvasJsModel.AreaChart()
            {
                Title = "Number of iPhones Sold in Different Quarters",
                AreaDataPoint = new List<AreDataPoint>
        {
            new AreDataPoint { X = new DateOnly(2015, 02, 1), Y = 74.4, Label = "Q1-2015" },
            new AreDataPoint { X = new DateOnly(2015, 05, 1), Y = 61.1, Label = "Q2-2015" },
            new AreDataPoint { X = new DateOnly(2015, 08, 1), Y = 47.0, Label = "Q3-2015" },
            new AreDataPoint { X = new DateOnly(2015, 11, 1), Y = 48.0, Label = "Q4-2015" },
            new AreDataPoint { X = new DateOnly(2016, 02, 1), Y = 74.8, Label = "Q1-2016" },
            new AreDataPoint { X = new DateOnly(2016, 05, 1), Y = 51.1, Label = "Q2-2016" },
            new AreDataPoint { X = new DateOnly(2016, 08, 1), Y = 40.4, Label = "Q3-2016" },
            new AreDataPoint { X = new DateOnly(2016, 11, 1), Y = 45.5, Label = "Q4-2016" },
            new AreDataPoint { X = new DateOnly(2017, 02, 1), Y = 78.3, Label = "Q1-2017", IndexLabel = "Highest", MarkerColor = "red" }
        }
            };

            return View(model);
        }
        #endregion
    }
}
