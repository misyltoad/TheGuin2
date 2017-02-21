using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class DiscordMessage : BaseMessage
    {
        public DiscordMessage(Discord.Message message)
        {
            messageInterface = message;
        }

        public override string GetText()
        {
            return messageInterface.RawText;
        }

        private Discord.Message messageInterface;
    }
}
