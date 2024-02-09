using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WaiPhyoMaungDontNetCore.MvcApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        public int Blog_Id { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Author { get; set; }
        public string? Blog_Content { get; set; }
    }
    #region PieChart
    public class PieChartModel
    {
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
    }
    #endregion

    #region BubbleChart
    public class BubbleChartModel
    {
        public List<BubbleSeries> Series { get; set; }
    }

    public class BubbleSeries
    {
        public string Name { get; set; }
        public List<BubbleData> Data { get; set; }
    }

    public class BubbleData
    {
        public DateTime X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
    #endregion

    #region rouped-stacked-column Chart
    public class ColumnChartModel
    {
        public List<ColumnSeries> Series { get; set; }
    }
    public class ColumnSeries
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public List<int> Data { get; set; }

    }
    #endregion

}
