using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2.Commands
{
    [OnMessage]
    class SeinfeldTheme
    {
        public SeinfeldTheme(BaseUser user, BaseServer server, BaseChannel channel, BaseMessage message)
		{
			if (message.GetText().ToLower().IndexOf("seinfeld") != -1 && message.GetText().ToLower().IndexOf("theme") != -1)
				channel.SendMessage("Relevant: https://www.youtube.com/watch?v=_V2sBURgUBI");
		}
    }
}
