using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace LYHMEManager
{
    public sealed class BrowserCommand
    {
        [XmlArrayItem("Line")]
        public string Name;
        public string Permission;
        public string Message;
        public string URL;
    }

    public sealed class RotateMap
    {
        [XmlArrayItem("Line")]
        public string Map;
    }


    public class LYHMEManagerConfiguration : IRocketPluginConfiguration
    {
        public string Key;
        public bool VehicleLicenseNeeded;
        public string KickMessage;
        public decimal LicensePrice;
        public bool RotateMap;

        [XmlArrayItem("Link")]
        [XmlArray(ElementName = "LinkManager")]
        public List<BrowserCommand> BrowseCommand;

        [XmlArrayItem("Maps")]
        [XmlArray(ElementName = "RotatingMaps")]
        public List<RotateMap> Maps;

        public void LoadDefaults()
        {
            Key = "Key-Here";
            VehicleLicenseNeeded = false;
            KickMessage = "You have been kicked for using antispy";
            LicensePrice = 500;
            RotateMap = false;

            BrowseCommand = new List<BrowserCommand>()
            {
                new BrowserCommand()
                {
                    Name ="website", Permission = "website", Message = "Gives link to website", URL = "http://lyhme.net/",
                }
            };

            Maps = new List<RotateMap>()
            {
                new RotateMap()
                {
                    Map = "PEI",
                }
            };
        }
    }
}