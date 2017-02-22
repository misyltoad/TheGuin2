using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2.Commands
{
    [OnUserJoined]
    class WelcomeMessage
    {
        public WelcomeMessage(BaseUser user, BaseServer server)
		{
			server.GetWelcomeChannel().SendMessage("Welcome " + user.GetTag() + " to the server! :slight_smile:");
		}
    }
}
