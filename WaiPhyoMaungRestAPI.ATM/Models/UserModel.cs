using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiPhyoMaungRestAPI.ATM.Models
{
    [Table("Tbl_User")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public int Balance {  get; set; }
    }
}
