using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheGuin2
{
	public class ConfigSystem
	{
		public ConfigSystem()
		{
		}

		public string GetText(string path)
		{
			try
			{
				return File.ReadAllText(StaticConfig.Paths.ConfigPath + "/" + path);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		/*public IList<T> DeserialiseFileToList<T>(string path)
		{
			string text = GetText(path);

			if (text != null && text != "")
			{
				IList<T> thingies = new List<T>();
				JsonTextReader reader = new JsonTextReader(new StringReader(text));
				reader.SupportMultipleContent = true;
				while (true)
				{
					if (!reader.Read())
						break;

					JsonSerializer serialiser = new JsonSerializer();
					T thing = serialiser.Deserialize<T>(reader);

					thingies.Add(thing);
				}

				return thingies;
			}
			else
				return null;
		}*/

		public T DeserialiseFile<T>(string path)
		{
			string text = GetText(path);

			if (text != null && text != "")
			{
				JsonTextReader reader = new JsonTextReader(new StringReader(text));
				reader.SupportMultipleContent = true;
				while (true)
				{
					if (!reader.Read())
						break;

					JsonSerializer serialiser = new JsonSerializer();
					T thing = serialiser.Deserialize<T>(reader);

					return thing;
				}
			}

			return default(T);
		}

		public void SerialiseToFile<T>(T thing, string path)
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			JsonWriter writer = new JsonTextWriter(sw);
			writer.Formatting = Formatting.Indented;
			JsonSerializer serialiser = new JsonSerializer();
			serialiser.Serialize(writer, thing);
			File.WriteAllText(StaticConfig.Paths.ConfigPath + "/" + path, sb.ToString());
		}
	}
}
