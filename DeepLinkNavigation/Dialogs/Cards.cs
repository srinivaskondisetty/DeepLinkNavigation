using DeepLinkNavigation.Utilities;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeepLinkNavigation.Dialogs
{
    public static class Cards
    {
        public static HeroCard GenerateDeepLinkAttachment(Activity activity)
        {
            return new HeroCard()
            {
                Title = "Deep Links",
                Text = "Deep links for static, conversations and configurable tabs",
                Buttons = new List<CardAction>()
                {
                    new CardAction()
                    {
                         DisplayText = "Static Tab",
                         Text = "Static Tab",
                         Title = "Static Tab",
                         Type = ActionTypes.OpenUrl,
                         Value = CommonUtility.GetDeepLink(activity, "static")
                    },
                    new CardAction()
                    {
                         DisplayText = "Configurable Tab",
                         Text = "Configurable Tab",
                         Title = "Configurable Tab",
                         Type = ActionTypes.OpenUrl,
                         Value = CommonUtility.GetDeepLink(activity, "channel")
                    },
                    new CardAction()
                    {
                         DisplayText = "Conversations",
                         Text = "Conversations",
                         Title = "Conversations",
                         Type = ActionTypes.OpenUrl,
                         Value = CommonUtility.GetDeepLink(activity, "conversation")
                    },
                }
            };
        }

        public static HeroCard GenerateDeepLinkPreviewAttachment()
        {
            return new HeroCard()
            {
                Title = "Deep Links",
                Text = "Deep links for static, conversations and configurable tabs"                
            };
        }
    }
}