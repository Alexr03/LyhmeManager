using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYHMEManager
{
    public class BrowserLayout : IRocketCommand
    {
        public string name;
        public string permission;
        public string message;
        public string url;


        public BrowserLayout(string commandName, string Permission, string commandMessage, string commandUrl)
        {
            name = commandName;
            permission = Permission;
            message = commandMessage;
            url = commandUrl;
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer User = (UnturnedPlayer)caller;

            User.Player.sendBrowserRequest(message, url.ToString());
        }

        public string Help
        {
            get { return "Plugin by Alexr03"; }
        }

        public string Name
        {
            get { return name; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { permission };
            }
        }

        public string Syntax
        {
            get { return ""; }
        }


        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }
    }
}