using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("echo", "Echo back!")]
    class EchoCommand : BaseCommand
    {
        public EchoCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
			channel.SendMessage(argsString);
        }
    }
}
