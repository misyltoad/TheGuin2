using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	public class ModuleListConfigSchema
	{
		public List<string> Modules { get; set; }
	}

	public class ModuleConfigDefault : ConfigDefault
	{
		public ModuleConfigDefault()
		{
			DefaultDir = "{}";
		}
	}

	public class ModuleListConfig : BaseConfig<ModuleListConfigSchema, ModuleConfigDefault>
	{
	}
}
