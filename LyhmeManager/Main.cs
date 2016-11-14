using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using System;
using System.IO;
using LyhmeManagerLibary;
using Rocket.Unturned;
using SDG.Unturned;
using Steamworks;
using Rocket.Unturned.Chat;
using Rocket.Core;

namespace LYHMEManager
{
    public class Main : RocketPlugin<LYHMEManagerConfiguration>
    {
        public static Main Instance;
        public static UnturnedPlayer user;
        CSteamID ID = (CSteamID)0;

        protected override void Load()
        {
            Keys.CheckKey(Configuration.Instance.Key);
            if(Keys.t)
            {
                Instance = this;

                Rocket.Core.Logging.Logger.LogWarning("Key Correct! Key: " + Configuration.Instance.Key + ", Welcome...");
                

                Rocket.Core.Logging.Logger.LogWarning("############################");
                Rocket.Core.Logging.Logger.LogWarning("LYHMEManager ~~ Alexr03");
                Rocket.Core.Logging.Logger.LogWarning("############################");
                Rocket.Core.Logging.Logger.LogWarning("LinkManager Configuration >>");

                foreach (BrowserCommand b in Configuration.Instance.BrowseCommand)
                {
                    BrowserLayout command = new BrowserLayout(b.Name, b.Permission, b.Message, b.URL);
                    R.Commands.Register(command);
                }

                foreach (BrowserCommand b in Configuration.Instance.BrowseCommand)
                {
                    Rocket.Core.Logging.Logger.LogWarning("Configuration ~~");
                    Rocket.Core.Logging.Logger.LogWarning("Name: " + b.Name);
                    Rocket.Core.Logging.Logger.LogWarning("Message: " + b.Message);
                    Rocket.Core.Logging.Logger.LogWarning("URL: " + b.URL);
                    Rocket.Core.Logging.Logger.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
                if(File.Exists(CommandCheckAnti.directory + "/User-Licenses.txt")) { }
                else { File.CreateText(CommandCheckAnti.directory + "/User-Licenses.txt"); }
                U.Events.OnPlayerConnected += OnPlayerConnected;

                Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerUpdateStance += OnPlayerUpdateStance;
                if (Configuration.Instance.RotateMap)
                {
                    Rocket.Core.Logging.Logger.Log("Maps in config:");

                    foreach (RotateMap h in Configuration.Instance.Maps)
                    {
                        Rocket.Core.Logging.Logger.Log(h.Map);
                    }
                }


                if (Configuration.Instance.RotateMap)
                {

                    System.Random r = new System.Random();
                    int index = r.Next(Configuration.Instance.Maps.Count);
                    RotateMap randomMap = Configuration.Instance.Maps[index];

                    Rocket.Core.Logging.Logger.Log("The current map is: " + Provider.map);
                    Rocket.Core.Logging.Logger.Log("Rolling the dice for next map....");
                    Rocket.Core.Logging.Logger.Log("The next map is: " + randomMap.Map);

                    try
                    {
                        string text = File.ReadAllText(CommandCheckAnti.directory + "/Server/Commands.dat");
                        text = text.Replace("map " + Provider.map, "map " + randomMap.Map);
                        File.WriteAllText(CommandCheckAnti.directory + "/Server/Commands.dat", text);
                        Rocket.Core.Logging.Logger.Log("Map in Commands.dat has changed to: " + randomMap.Map);
                    }
                    catch(Exception e)
                    {
                        Rocket.Core.Logging.Logger.Log("Error changing the map in the commands.dat the error is here:");
                        Rocket.Core.Logging.Logger.LogException(e);
                    }
                }
                else { Rocket.Core.Logging.Logger.Log("Rotate map is disabled"); }
            }
            else { Rocket.Core.Logging.Logger.Log("Incorrect Key... Unloading..."); Unload(); }
        }

        private void OnPlayerUpdateStance(UnturnedPlayer player, byte stance)
        {
            if (Configuration.Instance.VehicleLicenseNeeded)
            {
                if (stance == (byte)6)
                {
                    var content = File.ReadAllText(CommandCheckAnti.directory + "/User-Licenses.txt");
                    Rocket.Core.Logging.Logger.Log(player.CurrentVehicle.isDriver.ToString());
                    if (!player.CurrentVehicle.isDriver)
                    {
                        if (content.Contains(player.CSteamID.ToString())) { UnturnedChat.Say(player.CSteamID, "You have a license, drive safe!"); }
                        else
                        {
                            UnturnedChat.Say(player.CSteamID, "You do not have a license, you must buy one!");
                            player.CurrentVehicle.kickPlayer(0);
                        }
                    }
                }
            }
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            foreach(CSteamID c in CommandCheckAnti.BlacklistIDs)
            {
                if (player.CSteamID == c)
                {
                    Provider.kick(player.CSteamID, Configuration.Instance.KickMessage);
                }
            }
            
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("LYHMEAssets has unloaded.");
        }

        private void FixedUpdate()
        {


        }

        public static void ProcessUser()
        {
            foreach(UnturnedPlayer v in CommandCheckAnti.ToProcessPic)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(CommandCheckAnti.directory + "/Spy/" + v.CSteamID + ".jpg");

                if (img.Height == 639 || img.Width == 749)
                {
                    Rocket.Core.Logging.Logger.Log("User: " + v.CharacterName + " Screenshot matches fake screenshots!");
                    CommandCheckAnti.BlacklistIDs.Add(v.CSteamID);
                    using (StreamWriter w = File.AppendText(CommandCheckAnti.directory + "/AntiSpy-Users.txt"))
                    {
                        w.WriteLine("User: " + v.CharacterName + " Matches fake screenshots");
                        w.Close();
                    }
                    Provider.kick(v.CSteamID, "Using fake screenshots");
                }
                else
                {
                    Rocket.Core.Logging.Logger.Log("User " + v.CharacterName + " is not using fake screenshots");
                }
                img.Dispose();
            }
            CommandCheckAnti.ToProcessPic.Clear();
        }



        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                };
            }
        }


    }
}
