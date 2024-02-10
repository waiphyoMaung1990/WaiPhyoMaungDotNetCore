namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    public class HighChartModel
    {
        public class PieChartModel
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string[]? Categories { get; set; }
            public double[] Data { get; set; }
        }
        #region Pyramid
        public class Pyramid3D
        {
            public string ChartTitle { get; set; }
            public List<ChartData> SeriesData { get; set; }
            public string Description { get; set; }
        }

        public class ChartData
        {
            public string Name { get; set; }
            public int Y { get; set; }
        }
        #endregion

        #region Stacked and GroupColumn Chart
        public class StackedGroupColumn
        {
            public string ChartTitle { get; set; }
            public List<string> XAxisCategories { get; set; }
            public List<MedalSeries> Series { get; set; }
            public string Description { get; set; }
        }

        public class MedalSeries
        {
            public string Name { get; set; }
            public List<int> Data { get; set; }
            public string Stack { get; set; }
        }
        #endregion

        #region BasicArea
        public class BasicArea
        {
            public string ChartTitle { get; set; }
            public string Description { get; set; }
            public string Source { get; set; }
            public string RangeDescription { get; set; }
            public string YAxisTitle { get; set; }
            public int PointStart { get; set; }
            public int[] UsaData { get; set; }
            public int[] UssrRussiaData { get; set; }
        }
    #endregion


}
}


