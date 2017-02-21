using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	public class BotConfigSchema
	{
		public string CommandPrefix { get; set; }
	}

    public class BotConfigDefault : ConfigDefault
    {
        public BotConfigDefault()
        {
            DefaultDir = "{\n'CommandPrefix': '!'\n}";
        }
    }

    public class BotConfig : BaseConfig<BotConfigSchema, BotConfigDefault>
	{
		
	}
}
