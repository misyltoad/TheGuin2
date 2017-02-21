using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordChannel : BaseChannel
    {
        public DiscordChannel(Discord.Channel channel)
        {
            channelInterface = channel;
        }

        public override void SendMessage(string message)
        {
            channelInterface.SendMessage(message);
        }

        public override List<BaseUser> GetUsers()
        {
            List<BaseUser> users = new List<BaseUser>();
            for (int i = 0; i < channelInterface.Users.Count(); i++)
            {
                DiscordUser user = new DiscordUser(channelInterface.Users.ElementAt(i));
                users.Add(user);
            }
            return users;
        }

        public override BaseServer GetServer()
        {
            DiscordServer server = new DiscordServer(channelInterface.Server);
            return server;
        }

        public override string GetName()
        {
            return channelInterface.Name;
        }

        public override void SendFile(string filename)
        {
            channelInterface.SendFile(filename).Wait();
        }

        private Discord.Channel channelInterface;
    }
}
