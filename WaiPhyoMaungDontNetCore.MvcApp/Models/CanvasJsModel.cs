namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    public class CanvasJsModel
    {
        #region Piechart
        public class PieChart
        {
            public double[]? DataPoints { get; set; }
            public string[]? Labels { get; set; }
            public string? Title { get; set; }
        }
        #endregion
        #region PyramidChart
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
        #endregion

        #region AreaChart

        public class AreaChart
        {
            public string Title { get; set; }
            public List<AreDataPoint> AreaDataPoint { get; set; }
        }

        public class AreDataPoint
        {
            public DateOnly X { get; set; }
            public double Y { get; set; }
            public string Label { get; set; }
            public string IndexLabel { get; set; }
            public string MarkerColor { get; set; }
        }

        #endregion
        #region StackedBarChart
        public class StackedBarChart
        {
            public class DataPoint
            {
                public DateTime X { get; set; }
                public int Y { get; set; }
            }
            public List<DataPoint> Meals { get; set; }
            public List<DataPoint> Snacks { get; set; }
            public List<DataPoint> Drinks { get; set; }
            public List<DataPoint> Dessert { get; set; }
            public List<DataPoint> Takeaway { get; set; }
        }
        #endregion



    }
}
