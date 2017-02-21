using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public abstract class BaseCommand
    {
        public class CmdData
        {
            public BaseUser user;
            public BaseChannel channel;
            public BaseServer server;
            public BaseMessage message;

            public CmdData(BaseUser user, BaseChannel channel, BaseServer server, BaseMessage message)
            {
                this.user = user;
                this.channel = channel;
                this.server = server;
                this.message = message;
            }
        }

        public BaseCommand(CmdData data)
        {
            user = data.user;
            channel = data.channel;
            server = data.server;
            message = data.message;

            if (message.GetText().IndexOf(' ') > 0)
                args = message.GetText().Substring(message.GetText().IndexOf(' ') + 1).Split(' ');
            else
                args = new string[0];

            if (message.GetText().IndexOf(' ') > 0)
                argsString = message.GetText().Substring(message.GetText().IndexOf(' ') + 1);
            else
                argsString = "";

			try
			{
				Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

        public abstract void Execute();

        protected BaseUser user;
        protected BaseChannel channel;
        protected BaseServer server;
        protected BaseMessage message;
        protected string[] args;
        protected string argsString;
    }
}
