using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYHMEManager
{
    class CommandCheck : IRocketCommand
    {
     public UnturnedPlayer player2;

    public string Help
    {
        get { return "See's if the user is in godmode / vanish"; }
    }

    public string Name
    {
        get { return "check"; }
    }

    public string Syntax
    {
        get { return "/check <player>"; }
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
            return new List<string>() { "abuse.isgod" };
        }
    }

    public void Execute(IRocketPlayer caller, string[] command)
    {
            UnturnedPlayer user = (UnturnedPlayer)caller;
            if (command.Length == 1)
            {
                player2 = UnturnedPlayer.FromName(command[0]);
                if (player2.GodMode == true)
                {
                    UnturnedChat.Say(user.CSteamID, player2.CharacterName + " Godmode: True");
                }
                else
                {
                    UnturnedChat.Say(user.CSteamID, player2.CharacterName + " Godmode: False");
                }
            }

            if (player2.VanishMode == true)
            {
                UnturnedChat.Say(user.CSteamID, player2.CharacterName + " Vanish: True");
            }
            else
            {
                UnturnedChat.Say(user.CSteamID, player2.CharacterName + " Vanish: False");
            }

        }
    }
}
