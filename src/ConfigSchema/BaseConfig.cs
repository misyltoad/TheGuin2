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
			DefaultDir = "{\n}";
		}
	}

	public class BaseConfig<Schema> : BaseConfig<Schema, ConfigDefault>
	{ }

	public class BaseConfig<Schema, DefaultDir> where DefaultDir : ConfigDefault, new()
	{
		public static void Set(Schema newSchema, BaseServer server = null)
		{
            MakeDefault(server);

			Globals.GetConfigSystem().SerialiseToFile<Schema>(newSchema, GetRelativePath(server));
		}

        public static void Delete(BaseServer server = null)
        {
            string path = GetAbsolutePath(server);

            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string GetRelativePath(BaseServer server = null)
        {
            if (server != null)
                return String.Format("{0}/{1}{2}", server.GetConfigDir(), typeof(Schema).Name, ".json");
            else
                return String.Format("{0}{1}", typeof(Schema).Name, ".json");
        }

        private static string GetAbsolutePath(BaseServer server = null)
        {
            return String.Format("{0}/{1}", StaticConfig.Paths.ConfigPath, GetRelativePath(server));
        }

		private static void MakeDefault(BaseServer server = null)
		{
            string path = GetAbsolutePath(server);

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
            MakeDefault(server);

	        return Globals.GetConfigSystem().DeserialiseFile<Schema>(GetRelativePath(server));
		}
	}
}
