using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WaiPhyoMaungRestAPI.ATM.Models;

namespace WaiPhyoMaungRestAPI.ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region CreateUser

        [HttpPost]
        public IActionResult CreateUser(UserRequestModel _requestuser)
        {
            try
            {
                _requestuser.UserId = Guid.NewGuid().ToString();
                _appDbContext.User.Add(_requestuser);
                var result = _appDbContext.SaveChanges();

                if (result > 0)
                {
                    var responseModel = new UserResponseModel
                    {
                        UserId = _requestuser.UserId,
                        Message = "Account has been created.",
                    };

                    return Ok(responseModel);
                }
                else
                {
                    return BadRequest("Account Creating Failed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion

        #region CheckBalance

        [HttpGet]
        public IActionResult CheckBalance(string userId)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == userId);

            if (user is null)
            {
                return NotFound("User not found.");
            }

            try
            {
                var checkBalanceResponseModel = new CheckBalanceResponseModel
                {
                    IsSuccess = true,
                    Balance = user.Balance,
                    Message = "Balance retrieved successfully.",
                };

                return Ok(checkBalanceResponseModel);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
        #region Deposit

        [HttpPost]
        public IActionResult Deposit(DepositRequestModel _depositrequestmodel)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == _depositrequestmodel.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Balance += _depositrequestmodel.Amount;

            var result = _appDbContext.SaveChanges();
            var message = result > 0 ? $"Deposit Successful. Your current balance is {user.Balance}" : "Deposit Failed";
            var model = new DepositResponseModel
            {
                IsSuccess = result > 0,
                Message = message,
                Balance = user.Balance
            };
            return Ok(model);
        }

        #endregion

        #region Withdraw

        [HttpPost("Withdraw/{userId}/{amount}")]
        public IActionResult Withdraw(WithdrawRequestModel _withdrawRequestModel)
        {
            var user = _appDbContext.User.FirstOrDefault(x => x.UserId == _withdrawRequestModel.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Balance is 0 || user.Balance < _withdrawRequestModel.Amount)
            {
                return BadRequest("Insufficient funds.");
            }

            // Assuming the user has a default account for simplicity
            user.Balance -= _withdrawRequestModel.Amount;

            var result = _appDbContext.SaveChanges();
            var message = result > 0 ? $"Withdrawal Successful. Your current balance is {user.Balance}" : "Withdrawal Failed";
            var model = new WithdrawResponseModel
            {
                IsSuccess = result > 0,
                Message = message,
                Balance = user.Balance
            };
            return Ok(model);
        }

        #endregion
    }
}

