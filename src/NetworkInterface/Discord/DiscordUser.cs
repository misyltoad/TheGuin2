using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public override Bitmap GetAvatar()
        {
            try
            {
                string imageUrl = userInterface.AvatarUrl;
                var webClient = new System.Net.WebClient();
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                try
                {
                    Image image = Image.FromStream(new MemoryStream(imageBytes));
                    Bitmap bitmap = new Bitmap(image);
                    return bitmap;
                }
                catch
                {
                    Console.WriteLine("Invaid Avatar Format!");
                }
            }
            catch
            {
                Console.WriteLine("Invaid User!");
            }

            return null;
        }

        public override string GetUsername()
        {
            return userInterface.Name;
        }

        public override string GetTag()
        {
            return userInterface.Mention;
        }

        public override string GetHumanTag()
        {
            return String.Format("@{0}#{1}", GetUsername(), userInterface.Discriminator);
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

            return IsOwner() || userInterface.Id == DiscordConfig.OwnerId || isPermissionAdmin;
        }

		public override void GiveRole(BaseRole role)
		{
            if (role != null)
			    userInterface.AddRoles(((DiscordRole)role).roleInterface);
		}

        public override List<BaseRole> GetRoles()
        {
            List<BaseRole> roles = new List<BaseRole>();

            foreach (var role in userInterface.Roles)
                roles.Add(new DiscordRole(role));

            return roles;
        }

        public override string GetDataString()
        {
            return String.Format("``{0}`` (``{1}``|``{2}``|``{3}``)", GetTag(), GetHumanTag(), GetNickname(), GetUsername());
        }

        public override bool IsBotOwner()
        {
			return userInterface.Id == DiscordConfig.OwnerId;
		}

        public Discord.User userInterface;
    }
}
