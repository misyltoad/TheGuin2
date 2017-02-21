using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGuin2
{
    public class ModuleRegistry
    {
		private FileSystemWatcher watcher;

		public ModuleRegistry()
		{
			watcher = new FileSystemWatcher();
			watcher.Path = StaticConfig.Paths.ConfigPath;
			watcher.NotifyFilter = NotifyFilters.DirectoryName;

			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);
			watcher.Deleted += new FileSystemEventHandler(OnDeleted);

			watcher.EnableRaisingEvents = true;

			moduleList = new Dictionary<string, Module>();

			LoadAllFiles();
		}

		private void LoadAllFiles()
		{
			string[] allDirectories = Directory.GetDirectories(StaticConfig.Paths.ModulesPath);
			foreach (var filePath in allDirectories)
			{
				AddOrReplace(filePath);
			}
		}

		private void AddOrReplace(string key)
		{
			var keyName = key.Substring(StaticConfig.Paths.ModulesPath.Length + 1);

			if (moduleList.ContainsKey(keyName))
				moduleList.Remove(keyName);

			Module module = new Module(keyName);
			moduleList.Add(keyName, module);
		}

		private void OnChanged(object source, FileSystemEventArgs e)
		{
			AddOrReplace(e.FullPath);
		}

		private void OnDeleted(object source, FileSystemEventArgs e)
		{
			var key = e.FullPath.Substring(StaticConfig.Paths.ModulesPath.Length);
			if (moduleList.ContainsKey(key))
				moduleList.Remove(key);
		}

		private void OnRenamed(object source, RenamedEventArgs e)
		{
			var key = e.OldFullPath.Substring(StaticConfig.Paths.ModulesPath.Length);
			if (moduleList.ContainsKey(key))
				moduleList.Remove(key);

			AddOrReplace(e.FullPath);
		}

		public Module GetModule(string moduleName)
		{
			if (moduleList.ContainsKey(moduleName))
				return moduleList[moduleName];

			return null;
		}

		public string[] GetAvailableModules()
		{
			return moduleList.Keys.ToArray();
		}

		private Dictionary<string, Module> moduleList;
	}
}
