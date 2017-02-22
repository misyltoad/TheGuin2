using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGuin2
{
	public class DefaultRoleConfigSchema
	{
		public string DefaultRole { get; set; }
	}
	
	public class DefaultConfigDefault : ConfigDefault
	{
		public DefaultConfigDefault()
		{
			DefaultDir = "{\n'DefaultRole': '@everyone'\n}";
		}
	}
	
	public class DefaultRoleConfig : BaseConfig<DefaultRoleConfigSchema, DefaultConfigDefault>
	{
	}
	
    [OnCommand("setdefaultrole", "Set the default role for new members on the server!")]
    class SetDefaultRoleCommand : BaseCommand
    {
        public SetDefaultRoleCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
			if (!user.IsAdmin())
			{
				channel.SendMessage("You cant' do this, " + user.GetNickname() + "...");
				return;
			}
			
			if (args.Length > 0)
			{
				var role = server.FindRoleByName(argsString);
				
				if (role == null)
				{
					channel.SendMessage("I can't find that role.");
					return;
				}
				
				var roleConfig = DefaultRoleConfig.Get(server);
				
				if (roleConfig != null)
				{
					roleConfig.DefaultRole = role.GetId();
					DefaultRoleConfig.Set(roleConfig, server);
					channel.SendMessage("The default role is now " + role.GetName() + ".");
				}
				else
				{
					channel.SendMessage("Failed to get config.");
				}
				
				return;
			}
			
			DefaultRoleConfig.Delete(server);
			channel.SendMessage("The default role is now @everyone.");
        }
    }
	
	[OnUserJoined]
	class DefaultRoleAssigner
    {
        public DefaultRoleAssigner(BaseUser user, BaseServer server)
		{
			var roleConfig = DefaultRoleConfig.Get(server);
			if (roleConfig != null && roleConfig.DefaultRole != null && roleConfig.DefaultRole != "")
				user.GiveRole(server.FindRoleById(roleConfig.DefaultRole));
		}
    }
}
