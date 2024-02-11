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

    }
}
