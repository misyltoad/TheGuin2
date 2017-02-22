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
				string commandPrefix = BotConfig.CommandPrefix;
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
                    else
                    {
                        server.ExecuteHook(typeof(TheGuin2.OnMessage), user, server, channel, message);
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
            try
            {
                server.ExecuteHook(typeof(TheGuin2.OnUserBanned), user, server);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnUserUnbanned(BaseUser user, BaseServer server)
        {
            try
            {
                server.ExecuteHook(typeof(TheGuin2.OnUserUnbanned), user, server);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnUserChange(BaseUser oldUser, BaseUser newUser, BaseServer server)
        {
            try
            {
                server.ExecuteHook(typeof(TheGuin2.OnUserChange), oldUser, newUser, server);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnUserLeave(BaseUser user, BaseServer server)
        {
            try
            {
                server.ExecuteHook(typeof(TheGuin2.OnUserLeft), user, server);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
