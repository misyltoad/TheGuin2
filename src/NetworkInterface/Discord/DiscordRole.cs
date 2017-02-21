using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
	class DiscordRole : BaseRole
	{
		public Discord.Role roleInterface;
		public DiscordRole(Discord.Role role)
		{
			roleInterface = role;
		}

		public override string GetName()
		{
			return roleInterface.Name;
		}
		public override string GetId()
		{
			return roleInterface.Id.ToString();
		}
	}
}
