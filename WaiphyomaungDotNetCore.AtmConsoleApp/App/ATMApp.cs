using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.AtmConsoleApp.Domain.Entities;
using WaiphyomaungDotNetCore.AtmConsoleApp.Domain.Interfaces;
using WaiphyomaungDotNetCore.AtmConsoleApp.UI;

namespace WaiphyomaungDotNetCore.AtmConsoleApp.App
{
    public class ATMApp : IUserLogin, IUserAccountActions
    {
        private List<UserAccounts> _userAccoutLists;
        private UserAccounts selectedAccount;

        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            AppScreen.DisplayAppMenu();
            ProcessMenuoption();
        }

        public void InitializeData()
        {
            _userAccoutLists = new List<UserAccounts>
            {
                new UserAccounts{Id=1,FullName="wpm",AccountNumber=123456,CardNumber=111111,CardPin=222222,AccountBalance=50000.00m,IsLocked=false},
                new UserAccounts{Id=2,FullName="koko",AccountNumber=23456,CardNumber=111111,CardPin=321321,AccountBalance=10000.00m,IsLocked=false}
            };
        }

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccounts inputAcccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach (UserAccounts account in _userAccoutLists)
                {
                    selectedAccount = account;
                    if (inputAcccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;
                        if (inputAcccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;
                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                //print a lock message
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }

                }
                if (isCorrectLogin == false)
                {
                    Utility.PrintMessage("\n Invalid card number or PIN.", false);
                    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                    if (selectedAccount.IsLocked)
                    {
                        AppScreen.PrintLockScreen();
                    }

                }
                Console.Clear();
            }
        }
        private void ProcessMenuoption()
        {
            switch (Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;

                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;

                case (int)AppMenu.MakeWithdrawal:
                    MakeWithDrawal();
                    break;
              
                case (int)AppMenu.Logout:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage("You have successfully logout.Please collect your ATM Card.");
                    Run();
                    break;

                default:
                    Utility.PrintMessage("Invalid Option", false);
                    break;
            }
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is:{Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("How much would you like to deposit?");

            // to read a decimal amount for the deposit
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                // Add logic to handle the deposit, update account balance, etc.
                // For demonstration, let's just print a message with the formatted deposit amount
                string formattedDeposit = Utility.FormatAmount(depositAmount);
                Utility.PrintMessage($"You are depositing {formattedDeposit}.", true);
                Console.WriteLine("------------------");

                selectedAccount.AccountBalance += depositAmount; // Update the account balance

                Utility.PrintMessage($"Thank you for your deposit. Your new balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}");
                // Add logic to update the user's account balance or perform other deposit-related actions
                // For example: userAccount.UpdateBalance(depositAmount);
            }
            else
            {
                Utility.PrintMessage("Invalid input. Please enter a valid amount.", false);
                ProcessMenuoption();
            }
        }




        public void MakeWithDrawal()
        {
            Console.WriteLine("How much would you like to withdraw?");

            // to read a decimal amount for the withdrawal
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
            {

                // For demonstration, let's just print a message with the formatted withdrawal amount
                string formattedWithdrawal = Utility.FormatAmount(withdrawalAmount);

                // Check if the withdrawal amount is valid
                if (withdrawalAmount > 0 && withdrawalAmount <= selectedAccount.AccountBalance)
                {
                    selectedAccount.AccountBalance -= withdrawalAmount; // Update the account balance

                    Utility.PrintMessage($"You are withdrawing {formattedWithdrawal}.", true);
                    Console.WriteLine("------------------");
                    Utility.PrintMessage($"Thank you for your withdrawal. Your new balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}");
                    // Add logic to update the user's account balance or perform other withdrawal-related actions
                    // For example: userAccount.UpdateBalance(-withdrawalAmount);
                }
                else
                {
                    Utility.PrintMessage("Invalid withdrawal amount. Please enter a valid amount within your account balance.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid input. Please enter a valid amount.", false);
            }
            AppScreen.DisplayAppMenu();
            ProcessMenuoption(); // Continue processing the menu options
        }
    }
}
