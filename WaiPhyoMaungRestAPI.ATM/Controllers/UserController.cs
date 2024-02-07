using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WaiPhyoMaungRestAPI.ATM.Models;

namespace WaiPhyoMaungRestAPI.ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AppDbContext _appDbContext = new AppDbContext();

        #region CreateUser
        [HttpPost]
        public IActionResult CreateBlog(UserModel user)
        {
            _appDbContext.User.Add(user);
            var result = _appDbContext.SaveChanges();
            var message = result > 0 ? "Saving Successful." : "Saving Failed";
            return Ok(message);
        }
        #endregion

        #region CheckBalance
        [HttpGet("CheckBalance/{userId}")]
        public IActionResult CheckBalance(int userId)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var totalBalance = user.Balance;
            return Ok($"Total balance for user {userId}: {totalBalance}");
        }
        #endregion

        [HttpPost("Deposit/{userId}")]
        public IActionResult Deposit(int userId, int amount)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Assuming the user has a default account for simplicity
            user.Balance += amount;

            var result = _appDbContext.SaveChanges();
            var message = result > 0 ? $"Deposit Successful. Your current balance is {user.Balance}" : "Deposit Failed";
            return Ok(message);
        }
        #region Withdraw
        [HttpPost("Withdraw/{userId}")]
        public IActionResult Withdraw(int userId, int amount)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Balance is 0 || user.Balance < amount)
            {
                return BadRequest("Insufficient funds.");
            }

            // Assuming the user has a default account for simplicity
            user.Balance -= amount;

            var result = _appDbContext.SaveChanges();
            var message = result > 0 ? $"Withdrawal Successful. Your current balance is {user.Balance}" : "Withdrawal Failed";
            return Ok(message);
        }
        #endregion




    }
}
