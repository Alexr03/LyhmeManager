using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Timers;
using UnityEngine;

namespace LYHMEManager
{
    class CommandBuyLicense : IRocketCommand
    {
        public static string directory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName;

        public string Help
        {
            get { return ""; }
        }

        public string Name
        {
            get { return "buylicense"; }
        }

        public string Syntax
        {
            get { return "/buylicense"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "license.buy" };
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer user = (UnturnedPlayer)caller;
            var content = File.ReadAllText(CommandCheckAnti.directory + "/User-Licenses.txt");

            if (content.Contains(user.CSteamID.ToString())) { }
            else
            {
                LyhmeManagerLibary.License.BuyLicense(user.CSteamID.ToString(), -Main.Instance.Configuration.Instance.LicensePrice);
                using(StreamWriter w = File.AppendText(CommandCheckAnti.directory + "/User-Licenses.txt"))
                {
                    w.WriteLine(user.CSteamID.ToString());
                    w.Close();
                }
            }
        }
    }
}
