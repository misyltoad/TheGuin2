using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    class Program
    {
        static void Main(string[] args)
        {
			Task.Run(() => {
                DiscordWatcher discordWatcher = new DiscordWatcher(DiscordConfig.Get().LoginToken);
                discordWatcher.StartWatching();
                });

            while (true)
            {

            }
        }
    }
}
