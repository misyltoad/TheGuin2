using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	public class Module
	{
		public struct Meta
		{
			public string name;
		}

		public Meta meta;
		private DynamicSystem hookSystem;

		public Module(string name)
		{
			meta = new Meta();
			meta.name = name;

			hookSystem = new DynamicSystem(this);
		}

		public List<Assembly> GetAssemblies()
		{
			return hookSystem.GetAssemblies();
		}
	}
}
