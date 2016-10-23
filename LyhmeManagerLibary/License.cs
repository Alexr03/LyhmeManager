using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fr34kyn01535.Uconomy;
using Steamworks;
using System.IO;

namespace LyhmeManagerLibary
{
    public class License
    {
        public static void BuyLicense(string id, decimal price)
        {
            Uconomy.Instance.Database.IncreaseBalance(id, price);
        }

        public static void RevokeLicense(CSteamID csteamid)
        {

        }

    }
}
