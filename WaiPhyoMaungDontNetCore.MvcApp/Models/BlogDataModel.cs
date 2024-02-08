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

    public class PieChartModel
    {
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
    }

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

}
