using System.ComponentModel.DataAnnotations.Schema;

namespace WaiPhyoMaungDotNetCore.RealTimeChartUsingSignalR.Models
{
    [Table("Tbl_Team")]
    public class TeamDataModel
    {
        public int Id { get; set; } 
        public string TeamName { get; set; }    
        public int Score { get; set; }
    }
}
