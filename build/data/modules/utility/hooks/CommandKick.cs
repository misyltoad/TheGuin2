using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("kick", "Kicks a user!")]
    class KickCommand : BaseCommand
    {
        public KickCommand(CmdData data)
            : base(data)
        {}

        public override void Execute()
        {
            if (!user.IsAdmin())
            {
                user.SendMessage("You're not the boss of me!");
                return;
            }

            var foundUser = server.FindUser(argsString);
            if (foundUser == null)
            {
                channel.SendMessage("I don't know who that is...");
                return;
            }

            server.KickUser(foundUser);
            channel.SendMessage("The boot of justice has spoken for, " + foundUser.GetTag() + "!");
        }
    }
}
