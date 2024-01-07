using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaiphyomaungDotNetCore.AtmConsoleApp.UI;

namespace WaiphyomaungDotNetCore.AtmConsoleApp.App
{
    public class Entry
    {
        public static void Main(string[] args)
        {

            ATMApp atmApp = new ATMApp();
            atmApp.InitializeData();
            atmApp.Run();

        }
    }
}
