using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("slap", "Slap someone!")]
    class SlapCommand : BaseCommand
    {
        public SlapCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
			BaseUser user = null;
			
			if (argsString != "")
				user = server.FindUser(argsString);
			
			if (user != null)
				channel.SendMessage(String.Format("*slaps {0}*", user.GetTag()));
			else
				channel.SendMessage("I don't know who to slap...");
        }
    }
}
