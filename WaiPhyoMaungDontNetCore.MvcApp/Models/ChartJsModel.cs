using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    public class ChartJsModel
    {
        public class ColumnChart
        {
            public List<int>? Data { get; set; }
            public List<string>? Labels { get; set; }
        }
        public class LineChart
        {
            public string? Label { get; set; }
            public string? BorderColor { get; set; }
            public List<int>? Data { get; set; }

        }

        public class Doughnut
        {
            public string Label { get; set; }
            public List<string> BackgroundColor { get; set; }
            public List<int> Data { get; set; }
        }

        
    }
}
