using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.AtmConsoleApp.Domain.Entities;

namespace WaiphyomaungDotNetCore.AtmConsoleApp.UI
{
    public static class AppScreen
    {
        internal const string cur = "K ";
        internal static void Welcome()
        {
            //clears the console screen
            Console.Clear();
            //sets the title of the console window
            Console.Title = "ATM App Tutorial";
            Console.ForegroundColor = ConsoleColor.White;

            //sets the welcome message
            Console.WriteLine("\n\n----------------------Welcome to My ATM App-------------------");
            //prompt the user to insert atm card
            Console.WriteLine("Please insert your ATM card");

            Console.WriteLine("Note:Actual ATM machine will accept and validate,read the card number and validate it.");
            Utility.PressEnterToContinue();
        }

        internal static UserAccounts UserLoginForm()
        {
            UserAccounts tempUserAccount = new UserAccounts();

            tempUserAccount.CardNumber = Validator.Convert<long>("your card number.");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecrectInput("Enter your card PIN"));
            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN.....");
            Utility.PrintDotAnimation();
        }
        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your Account is Locked.Please go to the nearest branck" +
                "to unlock your account.Thank you.", true);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }
      internal static void WelcomeCustomer(string FullName  )
        {
            Console.WriteLine($"Welcome back,{FullName}");
            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-----My ATM App Menu--------");
            Console.WriteLine(":                          :");
            Console.WriteLine("1. Account Balance         :");
            Console.WriteLine("2. Cash Deposit            :");
            Console.WriteLine("3. Withdrawl               :");
            Console.WriteLine("4. Transfer                :");
            Console.WriteLine("5. Transactions            :");
            Console.WriteLine("6. Logout                  :");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("Thank you for using My ATM App.");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

       
    }
}
