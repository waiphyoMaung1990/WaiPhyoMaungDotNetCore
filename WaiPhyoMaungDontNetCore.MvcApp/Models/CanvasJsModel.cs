namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    public class CanvasJsModel
    {
        public class PieChart
        {
            public double[]? DataPoints { get; set; }
            public string[]? Labels { get; set; }
            public string? Title { get; set; }
        }
        public class PyramidChart
        {
            public string? Title { get; set; }
            public List<DataPoint>? DataPoints { get; set; }
        }

        public class DataPoint
        {
            public int? Y { get; set; }
            public string? Label { get; set; }
        }

    }
}
