using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordUser : BaseUser
    {
        public DiscordUser(Discord.User user)
        {
            userInterface = user;
        }

        public override string GetNickname()
        {
            if (userInterface.Nickname == null || userInterface.Nickname == "")
                return userInterface.Name;
            return userInterface.Nickname;
        }

        public override string GetUsername()
        {
            return userInterface.Name;
        }

        public override string GetTag()
        {
            return userInterface.Mention;
        }

        public override void SendMessage(string message)
        {
            userInterface.SendMessage(message);
        }

        public override bool IsOwner()
        {
            return userInterface.Server.Owner == userInterface;
        }

        public override string GetId()
        {
            return userInterface.Id.ToString();
        }

        public override bool IsAdmin()
        {
            bool isPermissionAdmin = false;
            foreach (var role in userInterface.Roles)
            {
                if (role.Permissions.Administrator)
                    isPermissionAdmin = true;
            }

            return IsOwner() || userInterface.Id == DiscordConfig.Get().OwnerId || isPermissionAdmin;
        }

		public override void GiveRole(BaseRole role)
		{
			userInterface.AddRoles(((DiscordRole)role).roleInterface);
		}

        public override bool IsBotOwner()
        {
			return userInterface.Id == DiscordConfig.Get().OwnerId;
		}

        private Discord.User userInterface;
    }
}
