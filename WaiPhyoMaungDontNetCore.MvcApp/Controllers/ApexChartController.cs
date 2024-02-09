using Microsoft.AspNetCore.Mvc;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using System;
using System.Collections.Generic;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        #region PieChart
        public IActionResult PieChart()
        {
            var model = new PieChartModel()
            {
                Labels = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" },
                Series = new List<int> { 44, 55, 13, 43, 22 },
            };

            return View(model);
        }
        #endregion

        #region BubbleChart
        public IActionResult BubbleChart()
        {
            var model = new BubbleChartModel()
            {
                Series = new List<BubbleSeries>
                {
                    new BubbleSeries
                    {
                        Name = "Product1",
                        Data = GenerateData(new DateTime(2017, 3, 11), 15, new { min = 20, max = 60 })
                    },
                    new BubbleSeries
                    {
                        Name = "Product2",
                        Data = GenerateData(new DateTime(2018, 4, 11), 15, new { min = 20, max = 60 })
                    },
                     new BubbleSeries
                    {
                        Name = "Product3",
                        Data = GenerateData(new DateTime(2019, 2, 11), 15, new { min = 20, max = 60 })
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
                var x = (int)(new Random().Next(1, 750)); // Equivalent to Math.floor(Math.random() * (750 - 1 + 1)) + 1
                int y = (int)new Random().Next((int)yrange.GetType().GetProperty("min").GetValue(yrange), (int)yrange.GetType().GetProperty("max").GetValue(yrange)); // Equivalent to Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min
                var z = (int)(new Random().Next(15, 75)); // Equivalent to Math.floor(Math.random() * (75 - 15 + 1)) + 15

                data.Add(new BubbleData { X = baseDate.AddDays(i), Y = y, Z = z });
                i++;
            }
            return data;
        }

        #endregion
        #region ColumnChart
        public IActionResult ColumnChart()
        {
            var model = new ColumnChartModel
            {
                Series = new List<ColumnSeries>
            {
                new ColumnSeries
                {
                    Name = "Q1 Budget",
                    Group = "budget",
                    Data = new List<int> { 44000, 55000, 41000, 67000, 22000, 43000 }
                },
                new ColumnSeries
                {
                    Name = "Q1 Actual",
                    Group = "actual",
                    Data = new List<int> { 48000, 50000, 40000, 65000, 25000, 40000 }
                },
                // Add more series as needed
            }
            };

            return View(model);
        }

    }
    #endregion


}
