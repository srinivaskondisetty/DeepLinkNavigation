using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeepLinkNavigation.Utilities
{
    public static class CommonUtility
    {
        public static string GetDeepLink(Activity activity, string type)
        {
            if (type == "channel")
            {
                var teamsChannelData = activity.GetChannelData<TeamsChannelData>();
                var teamId = teamsChannelData.Team == null ? "" : teamsChannelData.Team.Id;
                var channelId = teamsChannelData.Channel == null ? "" : teamsChannelData.Channel.Id;

                // The app ID, stored in the web.config file, should be the appID from your manifest.json file.
                var appId = System.Configuration.ConfigurationManager.AppSettings["MicrosoftAppId"];
                var entity = $"TabLink"; // Match the entity ID we setup when configuring the tab
                var tabContext = new TabContext()
                {
                    ChannelId = channelId,
                    CanvasUrl = "https://teams.microsoft.com"
                };

                string tabName = "tabname";
                var url = $"https://teams.microsoft.com/l/entity/{HttpUtility.UrlEncode(appId)}/{HttpUtility.UrlEncode(entity)}?label={HttpUtility.UrlEncode(tabName)}&context={HttpUtility.UrlEncode(JsonConvert.SerializeObject(tabContext))}";

                return url;
            }
            else if (type == "conversation")
            {
                // The app ID, stored in the web.config file, should be the appID from your manifest.json file.
                var BotId = System.Configuration.ConfigurationManager.AppSettings["BotId"];
                var TabEntityID = $"staticTab"; // Match the entity ID we setup when configuring the tab
                return "https://teams.microsoft.com/l/entity/28:" + BotId + "/conversation?conversationType=chat";
            }
            else
            {
                // The app ID, stored in the web.config file, should be the appID from your manifest.json file.
                var BotId = System.Configuration.ConfigurationManager.AppSettings["BotId"];
                var TabEntityID = $"statictab"; // Match the entity ID we setup when configuring the tab
                return "https://teams.microsoft.com/l/entity/28:" + BotId + "/" + TabEntityID + "?conversationType=chat";
            }
        }

    }

    internal class TabContext
    {
        public TabContext()
        {
        }

        public string ChannelId { get; set; }
        public string CanvasUrl { get; set; }
    }
}