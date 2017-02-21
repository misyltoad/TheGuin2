using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordWatcher : BaseWatcher
    {
        public DiscordWatcher(string token)
        {
            loginToken = token;
        }

        public override void StartWatching()
        {
            for (;;)
            {
                try
                {
                    DiscordInterface discord = new DiscordInterface(loginToken);
                }
                catch
                {

                }
            }
        }

        private string loginToken;
    }
}
