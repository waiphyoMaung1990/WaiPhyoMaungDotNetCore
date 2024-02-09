namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    public class HighChartModel
    {
        public class PieChartModel
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string[] Categories { get; set; }
            public double[] Data { get; set; }
        }
        public class BasicArea
        {
            public string ChartTitle { get; set; }
            public string ChartSubtitle { get; set; }
            public string Description { get; set; }
            public List<SeriesData> SeriesData { get; set; }
        }

        public class SeriesData
        {
            public string Name { get; set; }
            public List<int?> Data { get; set; }
        }


    }
}

