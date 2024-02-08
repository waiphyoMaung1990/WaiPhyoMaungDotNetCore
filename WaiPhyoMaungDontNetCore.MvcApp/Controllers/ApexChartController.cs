using Microsoft.AspNetCore.Mvc;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using System;
using System.Collections.Generic;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            var model = new PieChartModel()
            {
                Labels = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" },
                Series = new List<int> { 44, 55, 13, 43, 22 },
            };

            return View(model);
        }

        public IActionResult BubbleChart()
        {
            var model = new BubbleChartModel()
            {
                Series = new List<BubbleSeries>
                {
                    new BubbleSeries
                    {
                        Name = "Product1",
                        Data = GenerateData(new DateTime(2017, 2, 11), 20, new { min = 20, max = 60 })
                    },
                    new BubbleSeries
                    {
                        Name = "Product2",
                        Data = GenerateData(new DateTime(2018, 2, 11), 20, new { min = 20, max = 60 })
                    },
                    
                }
            };

            return View(model);
        }

        private List<BubbleData> GenerateData(DateTime baseDate, int count, object yrange)
        {
            var i = 0;
            var data = new List<BubbleData>();
            while (i < count)
            {
                var x = baseDate.AddDays(i);
                var y = new Random().Next((int)yrange.GetType().GetProperty("min").GetValue(yrange), (int)yrange.GetType().GetProperty("max").GetValue(yrange));
                var z = new Random().Next(15, 75);

                data.Add(new BubbleData { X = x, Y = y, Z = z });
                i++;
            }
            return data;
        }

    }
}
