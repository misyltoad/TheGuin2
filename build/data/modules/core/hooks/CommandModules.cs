using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    [OnCommand("modules", "Enable/Disable/List Modules.")]
    class ModulesCommand : BaseCommand
    {
        public ModulesCommand(CmdData data) : base(data)
        { }

        public override void Execute()
        {
			if (args.Length < 1 || args[0] == "" || args[0] == null)
			{
				var printString = "This server is using: ";
				foreach (var moduleName in server.GetModuleNames())
					printString += moduleName + ",";
					
				printString = printString.Substring(0, printString.Length - 1);
				if (user.IsAdmin())
				{
					printString += "\nYou can list available modules with, for example: \n";
					printString += "``" + BotConfig.Get().CommandPrefix + "modules list``\n";
					printString += "You can enable/disable modules with, for example: \n";
					printString += "``" + BotConfig.Get().CommandPrefix + "modules enable imagefun``\n";
					printString += "``" + BotConfig.Get().CommandPrefix + "modules disable imagefun``\n";
				}
				
				channel.SendMessage(printString);
				return;
			}
			
			if (!user.IsAdmin())
			{
				channel.SendMessage(String.Format("You aren't my master, {0}!", user.GetNickname()));
				return;
			}
			
			if (args[0].ToLower() == "list")
			{
				var printString = "Hello, " + user.GetNickname() + "!\nYour available modules are:";
				string[] modules = Globals.GetModuleRegistry().GetAvailableModules();
				
				foreach (var moduleName in modules)
				{
					printString += String.Format("\n - {0} {1}{2}", server.GetModuleNames().Contains(moduleName) ? "✅" : "🚫", moduleName, moduleName == "core" ? " [Required]" : "");
				}
				
				channel.SendMessage(printString);
				return;
			}
			
			if (args[0].ToLower() != "enable" && args[0].ToLower() != "disable")
			{
				channel.SendMessage(String.Format("I don't understand, what is \"{0}\", its not enable or disable, that's for sure!", args[0]));
				return;
			}
			
			bool validModule = false;
			if (args.Length == 2)
			{
				foreach(var enabledModule in Globals.GetModuleRegistry().GetAvailableModules())
				{
					if (args[1].ToLower() == enabledModule.ToLower())
						validModule = true;
				}
			}
			
			if (args.Length != 2 || !validModule)
			{
				channel.SendMessage(String.Format("I don't know what module to change, {0}!", user.GetNickname()));
				return;
			}
			
			if (args[0].ToLower() == "enable")
			{
				foreach(var enabledModule in server.GetModuleNames())
				{
					if (args[1].ToLower() == enabledModule)
					{
						channel.SendMessage("That module is already enabled, " + user.GetNickname() + "!");
						return;
					}
				}
				
				var moduleConfig = ModuleListConfig.Get(server);
				if (moduleConfig == null)
				{
					channel.SendMessage("Error accessing config database! 😢");
					return;
				}
				
				moduleConfig.Modules.Add(args[1].ToLower());
				ModuleListConfig.Set(moduleConfig, server);
				channel.SendMessage(args[1].ToLower() + " successfully enabled for you, " + user.GetNickname() + "!");
				return;
			} 
			else
			{				
				var moduleConfig = ModuleListConfig.Get(server);
				if (moduleConfig == null)
				{
					channel.SendMessage("Error accessing config database! 😢");
					return;
				}
				
				if (args[1].ToLower() == "core")
				{
					channel.SendMessage("You cannot disable the core module!");
					return;
				}
				
				if (!moduleConfig.Modules.Contains(args[1].ToLower()))
				{
					channel.SendMessage("The module: " + args[1].ToLower() + " is already disabled, " + user.GetNickname() + "!");
					return;
				}
				
				moduleConfig.Modules.Remove(args[1].ToLower());
				ModuleListConfig.Set(moduleConfig, server);
				channel.SendMessage(args[1].ToLower() + " successfully disabled for you, " + user.GetNickname() + "!");
				return;
			}
			
			//Globals.GetModuleRegistry().
        }
    }
}
