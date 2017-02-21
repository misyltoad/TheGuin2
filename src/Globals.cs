using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class Globals
    {
        private static Globals instance;
		private ConfigSystem configSystem;
		private ModuleRegistry moduleRegistry;

		private Globals()
        {
			configSystem = new ConfigSystem();
			moduleRegistry = new ModuleRegistry();
		}

        private static Globals GetInstance()
        {
            if (instance == null)
                instance = new Globals();

            return instance;
        }

		public static ConfigSystem GetConfigSystem()
		{
			return GetInstance().configSystem;
		}

		public static ModuleRegistry GetModuleRegistry()
		{
			return GetInstance().moduleRegistry;
		}
    }
}
