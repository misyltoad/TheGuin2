using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2.Commands
{
    [OnCommand("info", "Gets server info.")]
    class InfoCommand : BaseCommand
    {
        public InfoCommand(CmdData data) : base(data)
        { }

        public override void Execute()
        {
            string output = "I am TheGuin! Made by Joshua Ashton ( <@222447163727151104> )\n";
			output += "Want me on your server? Click here: http://bit.ly/2lsPr8T\n";
			output += "Info about " + server.GetName() + ":\n";
            output += "Owner: " + server.GetOwner().GetDataString() + "\n";
            output += "Members: " + server.GetUsers().Count + "\n";

            channel.SendMessage(output);
        }
    }
}
