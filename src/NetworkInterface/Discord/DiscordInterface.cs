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
                    Task.Run(() => OnMessageRecieved(user, channel, server, message));
                }
            };

            client.UserJoined += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                Task.Run(() => OnUserJoined(user, server));
            };

            client.UserBanned += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                Task.Run(() => OnUserBanned(user, server));
            };

            client.UserUnbanned += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                Task.Run(() => OnUserUnbanned(user, server));
            };

            client.UserLeft += (s, e) =>
            {
                DiscordUser user = new DiscordUser(e.User);
                DiscordServer server = new DiscordServer(e.Server);
                Task.Run(() => OnUserLeave(user, server));
            };

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(token, Discord.TokenType.Bot);
            });
        }

        private Discord.DiscordClient client;
    }
}
