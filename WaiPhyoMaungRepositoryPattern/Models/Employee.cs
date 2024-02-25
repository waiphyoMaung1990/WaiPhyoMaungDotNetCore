using System.ComponentModel.DataAnnotations.Schema;

namespace WaiPhyoMaungRepositoryPattern.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string city {  get; set; }  

    }
}
