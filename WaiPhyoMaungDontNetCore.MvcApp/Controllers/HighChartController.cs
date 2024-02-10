
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WaiPhyoMaungDontNetCore.MvcApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static WaiPhyoMaungDontNetCore.MvcApp.Models.HighChartModel;

namespace WaiPhyoMaungDontNetCore.MvcApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            var model = new HighChartModel.PieChartModel()
            {
                Title  = "Egg Yolk Composition",
                Subtitle  = "Source:<a href='https://www.mdpi.com/2072-6643/11/3/684/htm' target='_default'>MDPI</a>",
                Categories  = new string[] { "Water", "Fat", "Carbohydrates", "Protein", "Ash" },
                Data = new double[] { 55.02, 26.71, 1.09, 15.5, 1.68 }
            };

            return View(model);

        }

        public IActionResult Pyramid3D()
        {

            var model = new Pyramid3D
            {
                ChartTitle = "Highcharts Pyramid3D Chart",
                Description = "Highcharts supports drawing pyramid charts in 3D as well as 2D. While the 2D version is typically easier to read, the 3D version is sometimes used for decorative effect.",
                SeriesData = new List<ChartData>
                {
                    new() { Name = "Website visits", Y = 15654 },
                    new() { Name = "Downloads", Y = 4064 },
                    new() { Name = "Requested price list", Y = 1987 },
                    new() { Name = "Invoice sent", Y = 976 },
                    new() { Name = "Finalized", Y = 846 }
                }
            };
            return View(model);
        }

        public IActionResult StackedGroupColumn()
        {

            var model = new StackedGroupColumn
            {
                ChartTitle = "Olympic Games all-time medal table, grouped by continent",
                XAxisCategories = new List<string> { "Gold", "Silver", "Bronze" },
                Description = "Chart showing stacked columns with grouping, allowing specific series to be stacked on the same column. Stacking is often used to visualize data that accumulates to a sum.",
                Series = new List<MedalSeries>
                {
                    new() { Name = "Norway", Data = new List<int> { 148, 133, 124 }, Stack = "Europe" },
                    new() { Name = "Germany", Data = new List<int> { 102, 98, 65 }, Stack = "Europe" },
                    new() { Name = "United States", Data = new List<int> { 113, 122, 95 }, Stack = "North America" },
                    new() { Name = "Canada", Data = new List<int> { 77, 72, 80 }, Stack = "North America" }
                }
            };

            return View(model);

        }

        public IActionResult BasicAreaChart()
        {
            var model = new BasicArea
            {
                ChartTitle = "US and USSR Nuclear Stockpiles",
                Description = "Image description: An area chart compares the nuclear stockpiles of the USA and the USSR/Russia between 1945 and 2017...",
                Source = "FAS",
                RangeDescription = "Range: 1940 to 2017.",
                YAxisTitle = "Nuclear weapon states",
                PointStart = 1940,
                UsaData = new int[] {  0, 0, 0, 0, 0, 2, 9, 13, 50, 170, 299, 438, 841,
            1169, 1703, 2422, 3692, 5543, 7345, 12298, 18638, 22229, 25540,
            28133, 29463, 31139, 31175, 31255, 29561, 27552, 26008, 25830,
            26516, 27835, 28537, 27519, 25914, 25542, 24418, 24138, 24104,
            23208, 22886, 23305, 23459, 23368, 23317, 23575, 23205, 22217,
            21392, 19008, 13708, 11511, 10979, 10904, 11011, 10903, 10732,
            10685, 10577, 10526, 10457, 10027, 8570, 8360, 7853, 5709, 5273,
            5113, 5066, 4897, 4881, 4804, 4717, 4571, 4018, 3822, 3785, 3805,
            3750, 3708, 3708 },
                UssrRussiaData = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0,
            1, 5, 25, 50, 120, 150, 200, 426, 660, 863, 1048, 1627, 2492,
            3346, 4259, 5242, 6144, 7091, 8400, 9490, 10671, 11736, 13279,
            14600, 15878, 17286, 19235, 22165, 24281, 26169, 28258, 30665,
            32146, 33486, 35130, 36825, 38582, 40159, 38107, 36538, 35078,
            32980, 29154, 26734, 24403, 21339, 18179, 15942, 15442, 14368,
            13188, 12188, 11152, 10114, 9076, 8038, 7000, 6643, 6286, 5929,
            5527, 5215, 4858, 4750, 4650, 4600, 4500, 4490, 4300, 4350, 4330,
            4310, 4495, 4477 }
            };

            return View(model);
        }

    }

}

