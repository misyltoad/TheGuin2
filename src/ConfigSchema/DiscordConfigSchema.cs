using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	public class DiscordConfigSchema
	{
		public ulong OwnerId { get; set; }
		public string LoginToken { get; set; }
	}

    public class DiscordConfigDefault : ConfigDefault
    {
        public DiscordConfigDefault()
        {
            DefaultDir = "{\n'OwnerId': 'youridhere',\n'LoginToken': 'thebotlogintokenhere'\n}";
        }
    }

    public class DiscordConfig : BaseConfig<DiscordConfigSchema, DiscordConfigDefault>
	{

	}
}
