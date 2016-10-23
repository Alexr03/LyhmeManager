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
    class CommandGiveLicense : IRocketCommand
    {
        public static string directory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName;

        public string Help
        {
            get { return ""; }
        }

        public string Name
        {
            get { return "givelicense"; }
        }

        public string Syntax
        {
            get { return ""; }
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
                return new List<string>() { "license.give" };
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer user = (UnturnedPlayer)caller;

            UnturnedPlayer give_to = UnturnedPlayer.FromName(command[0]);

            var content = File.ReadAllText(CommandCheckAnti.directory + "/User-Licenses.txt");

            if (content.Contains(give_to.CSteamID.ToString())) { }
            else
            {
                LyhmeManagerLibary.License.BuyLicense(give_to.CSteamID.ToString(), -Main.Instance.Configuration.Instance.LicensePrice);
                using(StreamWriter w = File.AppendText(CommandCheckAnti.directory + "/User-Licenses.txt"))
                {
                    w.WriteLine(give_to.CSteamID.ToString());
                    w.Close();
                }
            }
        }
    }
}
