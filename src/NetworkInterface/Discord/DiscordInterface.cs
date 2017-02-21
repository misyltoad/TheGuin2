using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordInterface : BaseInterface
    {
        public DiscordInterface(string token)
        {
			if (token == "" || token == null)
			{
				Console.WriteLine("Couldn't get login token!");
				return;
			}

            client = new Discord.DiscordClient();
            client.MessageReceived += (s, e) =>
            {
                if (!e.Message.IsAuthor)
                {
                    DiscordUser user = new DiscordUser(e.User);
                    DiscordChannel channel = new DiscordChannel(e.Channel);
                    DiscordServer server = new DiscordServer(e.Server);
                    DiscordMessage message = new DiscordMessage(e.Message);
                    OnMessageRecieved(user, channel, server, message);
                }
            };

            client.UserJoined += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                OnUserJoined(user, server);
            };

            client.UserBanned += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                OnUserBanned(user, server);
            };

            client.UserUnbanned += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                OnUserUnbanned(user, server);
            };

            client.UserLeft += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                OnUserLeave(user, server);
            };

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(token, Discord.TokenType.Bot);
            });
        }

        private Discord.DiscordClient client;
    }
}
