using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	public static class StaticConfig
	{
		public static class Paths
		{
			public static readonly string DataPath = "data";
			public static readonly string ConfigPath = DataPath + "/config";
			public static readonly string ModulesPath = DataPath + "/modules";
			public static readonly string TempPath = DataPath + "/temp";
		}
	}
}
