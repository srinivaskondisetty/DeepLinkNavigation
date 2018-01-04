using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace DeepLinkNavigation.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text == "help")
            {
                // return our reply to the user
                await context.PostAsync($"To get the deep links of static, configurable tab and 1:1 chat Please type get link command");
            }
            else if (activity.Text == "getlink")
            {
                HeroCard card = Cards.GenerateDeepLinkAttachment(activity);
                var reply = context.MakeMessage();
                reply.Attachments.Add(card.ToAttachment());

                // return our reply to the user
                await context.PostAsync(reply);
            }
            else
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                // return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}