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

            if (name == null || name == "" || users == null)
                return null;

            // First pass of literal search tests.
            foreach (var user in users)
            {
                if (user.Mention != null && name == user.Mention)
                    return new DiscordUser(user);

                if (user.NicknameMention != null && name == user.Mention)
                    return new DiscordUser(user);

                if (user.Nickname != null && name == user.Nickname)
                    return new DiscordUser(user);

                if (user.Name != null && name == user.Name)
                    return new DiscordUser(user);
            }

            // First pass of lower search tests, removing mentions as this is not possible.
            foreach (var user in users)
            {
                if (user.Nickname != null && name.ToLower() == user.Nickname.ToLower())
                    return new DiscordUser(user);

                if (user.Name != null && name.ToLower() == user.Name.ToLower())
                    return new DiscordUser(user);
            }


            // Second pass for literal search test, removing mentions for search
            foreach (var user in users)
            {
                if (user.Nickname != null && user.Nickname.IndexOf(name) != -1)
                    return new DiscordUser(user);

                if (user.Name != null && user.Name.IndexOf(name) != -1)
                    return new DiscordUser(user);
            }

            // Second pass for lower search test, removing mentions for search
            foreach (var user in users)
            {
                if (user.Nickname != null && user.Nickname.ToLower().IndexOf(name.ToLower()) != -1)
                    return new DiscordUser(user);

                if (user.Name != null && user.Name.ToLower().IndexOf(name.ToLower()) != -1)
                    return new DiscordUser(user);
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

        public override BaseChannel FindChannelByName(string name)
        {
            if (name == null)
                return null;

            foreach (var channel in serverinterface.TextChannels)
            {
                if (channel.Name.ToLower() == name.ToLower())
                    return new DiscordChannel(channel);
            }

            foreach (var channel in serverinterface.TextChannels)
            {
                try
                {
                    if (channel.Name.Substring(0, name.Length).ToLower() == name.ToLower())
                        return new DiscordChannel(channel);
                }
                catch { }
            }

            return null;
        }

        public override BaseRole FindRoleById(string id)
		{
			if (id == null)
				return null;

            try
            {
                foreach (var role in serverinterface.Roles)
                {
                    if (role.Id == ulong.Parse(id))
                        return new DiscordRole(role);
                }
            }
            catch { }

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

        public override void BanUser(BaseUser user)
        {
            serverinterface.Ban(((DiscordUser)user).userInterface, 1);
        }
        public override void KickUser(BaseUser user)
        {
            ((DiscordUser)user).userInterface.Kick();
        }

        private Discord.Server serverinterface;
    }
}
