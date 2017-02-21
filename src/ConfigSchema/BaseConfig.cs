using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGuin2
{
	public class ConfigDefault
	{
		public string DefaultDir;
	}

	public class DefaultConfigDefault : ConfigDefault
	{
		public DefaultConfigDefault()
		{
			DefaultDir = "{}";
		}
	}

	public class BaseConfig<Schema> : BaseConfig<Schema, ConfigDefault>
	{ }

	public class BaseConfig<Schema, DefaultDir> where DefaultDir : ConfigDefault, new()
	{
		public static void Set(Schema newSchema, BaseServer server = null)
		{
			MakeDir(server);

			if (server != null)
				Globals.GetConfigSystem().SerialiseToFile<Schema>(newSchema, server.GetConfigDir() + typeof(Schema).Name + ".json");
			else
				Globals.GetConfigSystem().SerialiseToFile<Schema>(newSchema, typeof(Schema).Name + ".json");
		}

		private static void MakeDir(BaseServer server = null)
		{
			string path = "";
			if (server != null)
				path = StaticConfig.Paths.ConfigPath + "/" + server.GetConfigDir() + "/" + typeof(Schema).Name + ".json";
			else
				path = StaticConfig.Paths.ConfigPath + "/" + typeof(Schema).Name + ".json";

			try
			{
				if (!File.Exists(path))
					File.WriteAllText(path, new DefaultDir().DefaultDir);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static Schema Get(BaseServer server = null)
		{
			MakeDir(server);

			if (server != null)
				return Globals.GetConfigSystem().DeserialiseFile<Schema>(server.GetConfigDir() + "/" + typeof(Schema).Name + ".json");
			else
				return Globals.GetConfigSystem().DeserialiseFile<Schema>(typeof(Schema).Name + ".json");
		}
	}
}
