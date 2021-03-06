﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using System.Collections.Generic;
using DeepLinkNavigation.Utilities;

namespace DeepLinkNavigation
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                Activity strippedActivity = Middleware.Middleware.StripAtMentionText(activity);
                await Conversation.SendAsync(strippedActivity, () => new Dialogs.RootDialog());
            }
            else if (activity.Type == ActivityTypes.Invoke) // Received an invoke
            {
                Activity strippedActivity = Middleware.Middleware.StripAtMentionText(activity);
                var invokeResponse = this.GetComposeExtensionResponse(strippedActivity);
                return Request.CreateResponse<ComposeExtensionResponse>(HttpStatusCode.OK, invokeResponse);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private ComposeExtensionResponse GetComposeExtensionResponse(Activity activity)
        {
            var composeExtensionQuery = activity.GetComposeExtensionQueryData();
            var composeExtParamValue = composeExtensionQuery.Parameters[0].Value.ToString();
            // Process data and return the response.
            ComposeExtensionResponse composeExtensionResponse = new ComposeExtensionResponse();
            ComposeExtensionResult composeExtensionResult = new ComposeExtensionResult();
            List<ComposeExtensionAttachment> lstComposeExtensionAttachment = new List<ComposeExtensionAttachment>();
            string imageUrl = "https://luna1.co/cae4f2.png";
            string deepLinkUrl = TemplateUtility.GetDeepLink(activity, composeExtParamValue);
            lstComposeExtensionAttachment.Add(TemplateUtility.GenerateComposeExtentionAttachments(activity));
            composeExtensionResult.Type = "result";
            composeExtensionResult.AttachmentLayout = "list";
            composeExtensionResult.Attachments = lstComposeExtensionAttachment;
            composeExtensionResponse.ComposeExtension = composeExtensionResult;
            return composeExtensionResponse;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}