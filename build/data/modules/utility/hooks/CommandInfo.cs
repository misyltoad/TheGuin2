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
            string output = "Info about " + server.GetName() + ":\n";
            output += "Owner: " + server.GetOwner().GetTag() + " (" + server.GetOwner().GetNickname() + ")\n";
            output += "Members: " + server.GetUsers().Count + "\n";

            channel.SendMessage(output);
        }
    }
}
