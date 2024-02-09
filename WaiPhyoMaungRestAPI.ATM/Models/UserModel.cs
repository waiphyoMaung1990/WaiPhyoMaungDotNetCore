using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaiPhyoMaungRestAPI.ATM.Models
{
    [Table("Tbl_User")]
    public class UserRequestModel
    {
        [Key]
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public decimal Balance { get; set; }
    }


    public class UserResponseModel
    {
        public string UserId { get; set; }
        public string? Message { get; set; }

    }
    public class DepositResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public decimal Balance { get; set; }
    }

    public class DepositRequestModel
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawRequestModel
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public decimal Balance { get; set; }
    }

    public class CheckBalanceRequestModel
    {
        public string UserId { get; set; }
    }

    public class CheckBalanceResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public decimal Balance { get; set; }
    }
}
