using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("ping", "Pings back!")]
    class PingCommand : BaseCommand
    {
        public PingCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
            channel.SendMessage("Pong!");
        }
    }
}
