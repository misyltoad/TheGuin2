using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGuin2
{
	public class DefaultRoleConfig
	{
		public class DefaultRoleConfigSchema
		{
			public string DefaultRole { get; set; }
		}

		public static void Set(DefaultRoleConfigSchema newSchema, BaseServer server)
		{
			MakeDir(server);
			Globals.GetConfigSystem().SerialiseToFile<DefaultRoleConfigSchema>(newSchema, server.GetConfigDir() + "/defaultroleconfig.json");
		}

		private static void MakeDir(BaseServer server)
		{
			var path = StaticConfig.Paths.ConfigPath + "/" + server.GetConfigDir() + "/defaultroleconfig.json";
			try
			{
				if (!File.Exists(path))
					File.WriteAllText(path, "{}");
			}
			catch (Exception e)
			{
			}
		}

		public static DefaultRoleConfigSchema Get(BaseServer server)
		{
			MakeDir(server);
			return Globals.GetConfigSystem().DeserialiseFile<DefaultRoleConfigSchema>(server.GetConfigDir() + "/defaultroleconfig.json");
		}
	}
	
    [OnCommand("setdefaultrole", "Set the default role for new members on the server!")]
    class SetDefaultRoleCommand : BaseCommand
    {
        public SetDefaultRoleCommand(CmdData data) : base(data)
        {}

        public override void Execute()
        {
			if (args.Length > 0)
			{
				var role = server.FindRoleByName(argsString);
				
				var roleConfig = DefaultRoleConfig.Get(server);
				roleConfig.DefaultRole = role.GetId();
				DefaultRoleConfig.Set(roleConfig, server);
				
				return;
			}
			
			try
			{
				File.Delete(StaticConfig.Paths.ConfigPath + "/" + server.GetConfigDir() + "/defaultroleconfig.json");
			}
			catch{}
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
