using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiphyomaungDotNetCore.AtmConsoleApp.Domain.Interfaces
{
    public interface IUserAccountActions
    {
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithDrawal();

    }
}
