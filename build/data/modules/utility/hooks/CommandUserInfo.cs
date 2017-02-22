using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("userinfo", "Gets all details of a user.")]
    class UserInfoCommand : BaseCommand
    {
        public UserInfoCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
			BaseUser foundUser;
			if (argsString != null && argsString != "")
			{
				foundUser = server.FindUser(argsString);
				if (foundUser == null)
				{
					channel.SendMessage("Cannot find that user");
					return;
				}
			}
			else
				foundUser = user;
			
			channel.SendMessage(foundUser.GetDataString());
        }
    }
}
