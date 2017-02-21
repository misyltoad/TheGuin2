using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public abstract class BaseInterface
    {
        public void OnMessageRecieved(BaseUser user, BaseChannel channel, BaseServer server, BaseMessage message)
        {
			try
			{
				string messageText = message.GetText();
				string commandPrefix = BotConfig.Get().CommandPrefix;
				if (messageText.Length > commandPrefix.Length)
				{
					if (messageText.Substring(0, commandPrefix.Length) == commandPrefix)
					{
						string commandName = messageText.Substring(commandPrefix.Length);
						if (commandName.IndexOf(' ') > -1)
							commandName = commandName.Substring(0, commandName.IndexOf(' '));

						//Globals.GetCommandRegistry().ExecuteCommand(commandName, user, channel, server, message);
						List<Type> types = server.GetAllValidTypesWithAttribute(typeof(OnCommand));
						if (types != null)
						{
							foreach (var type in types)
							{
								bool valid = false;
								OnCommand[] attributes = (OnCommand[])type.GetCustomAttributes(typeof(OnCommand), true);
								foreach (var attribute in attributes)
								{
									if (attribute.name.ToLower() == commandName.ToLower())
										valid = true;
								}

								if (valid)
								{
									BaseCommand.CmdData commandData = new BaseCommand.CmdData(user, channel, server, message);
									Activator.CreateInstance(type, commandData);
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

        public void OnUserJoined(BaseUser user, BaseServer server)
        {
			try
			{
				server.ExecuteHook(typeof(TheGuin2.OnUserJoined), user, server);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

        public void OnUserBanned(BaseUser user, BaseServer server)
        {
            //server.GetBotChannel().SendMessage("🔨 ***" + user.GetTag() + " has been smited!*** 🔨");
        }

        public void OnUserUnbanned(BaseUser user, BaseServer server)
        {
            //server.GetBotChannel().SendMessage("👼 ***" + user.GetTag() + " has been forgiven!*** 👼");
        }

        public void OnUserLeave(BaseUser user, BaseServer server)
        {
           // server.GetBotChannel().SendMessage("😢 ***" + user.GetTag() + " has left... Goodbye.*** 😢");
        }
    }
}
