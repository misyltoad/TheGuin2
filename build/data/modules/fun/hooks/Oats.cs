using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2.Commands
{
    [OnMessage]
    class Oats
    {
        public Oats(BaseUser user, BaseServer server, BaseChannel channel, BaseMessage message)
		{
			if (message.GetText().ToLower().IndexOf("oats") != -1 || message.GetText().ToLower().IndexOf("öats") != -1)
				channel.SendMessage("pass me the öats brother\nhttps://www.youtube.com/watch?v=8I1sQlRiJdY");
		}
    }
}
