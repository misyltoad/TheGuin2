using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGuin2
{
    public abstract class BaseServer
    {
        public abstract List<BaseChannel> GetChannels();
        public abstract List<BaseUser> GetUsers();

        public BaseChannel GetBotChannel()
        {
            var channels = GetChannels();
            foreach (var channel in channels)
            {
                if (channel.GetName().Substring(0, 3) == "bot")
                    return channel;
            }

            if (channels.Count > 0)
                return channels[1];

            return channels[0];
        }

        public abstract BaseUser FindUser(string name);

        public BaseChannel GetWelcomeChannel()
        {
            var channels = GetChannels();
            if (channels.Count > 0)
                return channels[1];

            return channels[0];
        }

		public string GetConfigDir()
		{
			try
			{
				Directory.CreateDirectory(StaticConfig.Paths.ConfigPath + "/server");
				Directory.CreateDirectory(StaticConfig.Paths.ConfigPath + "/server/" + GetId());
			}
			catch
			{ }

			return "server/" + GetId();
		}

		public List<string> GetModuleNames()
		{
			List<string> moduleNames = new List<string>();

			try
			{
				moduleNames.AddRange(ModuleListConfig.Get(this).Modules);
			}
			catch
			{
				moduleNames.Add("core");
			}

			return moduleNames;
		}

		public abstract BaseRole FindRoleByName(string name);
		public abstract BaseRole FindRoleById(string name);

		public List<Type> GetAllValidTypesWithAttribute(Type hook)
		{
			try
			{
				List<Type> types = new List<Type>();
				foreach (var module in GetModuleNames())
				{
					Module realModule = Globals.GetModuleRegistry().GetModule(module);
					if (realModule != null)
					{
						List<Assembly> assemblies = realModule.GetAssemblies();
						foreach (var assembly in assemblies)
						{
							foreach (Type type in assembly.GetTypes())
							{
								object[] attributes = type.GetCustomAttributes(hook, true);
								if (attributes.Length > 0)
									types.Add(type);
							}
						}
					}

				}

				return types;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		public void ExecuteHook(Type hook, params object[] args)
		{
			try
			{
				List<Type> types = GetAllValidTypesWithAttribute(hook);

				if (types != null)
				{
					foreach (var type in types)
					{
						Activator.CreateInstance(type, args);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

        public abstract BaseUser GetOwner();
        public abstract string GetName();
        public abstract string GetId();

		public abstract List<BaseUser> GetBans();
    }
}
