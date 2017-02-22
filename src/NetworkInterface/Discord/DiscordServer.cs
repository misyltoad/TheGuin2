using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordServer : BaseServer
    {
        public DiscordServer(Discord.Server server)
        {
            serverinterface = server;
        }

        public override List<BaseChannel> GetChannels()
        {
            List<Discord.Channel> channels = new List<Discord.Channel>();
            foreach (var channel in serverinterface.TextChannels)
                channels.Add(channel);

            List<Discord.Channel> sortedChannels = channels.OrderBy(o => o.Position).ToList();
            List<BaseChannel> interfaceChannels = new List<BaseChannel>();

            foreach (var channel in sortedChannels)
                interfaceChannels.Add(new DiscordChannel(channel));

            return interfaceChannels;
        }

		public override string GetId()
		{
			return serverinterface.Id.ToString();
		}

        public override List<BaseUser> GetUsers()
        {
            List<BaseUser> users = new List<BaseUser>();
            for (int i = 0; i < serverinterface.Users.Count(); i++)
            {
                DiscordUser user = new DiscordUser(serverinterface.Users.ElementAt(i));
                users.Add(user);
            }
            return users;
        }

        public override BaseUser GetOwner()
        {
            return new DiscordUser(serverinterface.Owner);
        }

        public override string GetName()
        {
            if (serverinterface != null)
                return serverinterface.Name;

            return "";
        }

        public override BaseUser FindUser(string name)
        {
            var users = serverinterface.Users;
            foreach (var user in users)
            {
                try
                {
                    if ((user.Nickname != null && user.Nickname == name.ToLower()) || (user.Name != null && user.Name.ToLower() == name.ToLower()) || user.Mention == name)
                        return new DiscordUser(user);
                }
                catch { }
            }

            foreach (var user in users)
            {
                try
                {
                    if (user.Nickname != null && user.Nickname.Length >= name.Length && user.Nickname.Substring(0, name.Length).ToLower() == name.ToLower())
                        return new DiscordUser(user);
                } catch { }
            }


            foreach (var user in users)
            {
                try
                {
                    if (user.Name != null && user.Name.Length >= name.Length && user.Name.Substring(0, name.Length).ToLower() == name.ToLower())
                        return new DiscordUser(user);
                }
                catch { }
            }

            return null;
        }

		public override BaseRole FindRoleByName(string name)
		{
			if (name == null)
				return null;

			foreach (var role in serverinterface.Roles)
			{
				if (role.Name.ToLower() == name.ToLower())
					return new DiscordRole(role);
			}

			foreach (var role in serverinterface.Roles)
			{
                try
                {
                    if (role.Name.Substring(0, name.Length).ToLower() == name.ToLower())
                        return new DiscordRole(role);
                }
                catch { }
			}

			return null;
		}


		public override BaseRole FindRoleById(string id)
		{
			if (id == null)
				return null;

			foreach (var role in serverinterface.Roles)
			{
				if (role.Id == ulong.Parse(id))
					return new DiscordRole(role);
			}

			return null;
		}

		public override List<BaseUser> GetBans()
        {
                List<BaseUser> interfaceBans = new List<BaseUser>();

                var bans = serverinterface.GetBans().ContinueWith(t =>
                {
                    try
                    {
                        foreach (var ban in t.Result)
                        {
                            interfaceBans.Add(new DiscordUser(ban));
                        }
                    } catch
                    {
                        interfaceBans = null;
                    }
                });

                bans.Wait();
                return interfaceBans;
        }

        private Discord.Server serverinterface;
    }
}
