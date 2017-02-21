using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("help", "I NEED ASSISTANCE!")]
    class HelpCommand : BaseCommand
    {
        public HelpCommand(CmdData data) : base(data)
        { }

        public override void Execute()
        {
            string output = "```Help for TheGuin for " + server.GetName() + ":\n";
			List<Type> types = server.GetAllValidTypesWithAttribute(typeof(OnCommand));
            foreach (var type in types)
            {
				OnCommand[] attributes = (OnCommand[])type.GetCustomAttributes(typeof(OnCommand), true);
				foreach (var attribute in attributes)
					output += BotConfig.Get().CommandPrefix + attribute.name + " -> " + attribute.description + "\n";
            }
            output += "Have fun!```";

            user.SendMessage(output);
        }
    }
}
