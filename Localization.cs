using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Raytracer
{
	public enum Language
	{
		English,
		Czech
	}

	public static class Localization
	{
		private static Language current;
		private static Dictionary<string, string> entries;

		static Localization()
		{
			SetLanguage(Language.Czech);
		}

		public static void SetLanguage(Language language)
		{
			entries = new Dictionary<string, string>();
			current = language;

			entries = language switch
			{
				Language.English => ParseFile("Assets/Language/English.lang").ToDictionary(pair => pair.Key, pair => pair.Value),
				Language.Czech => ParseFile("Assets/Language/Czech.lang").ToDictionary(pair => pair.Key, pair => pair.Value),
				_ => throw new Exception($"Unsupported language: {language}")
			};
		}

		private static IEnumerable<KeyValuePair<string, string>> ParseFile(string path)
		{
			string[] lines = File.ReadAllLines(path);
			foreach (string line in lines)
			{
				string[] pair = line.Split('=');
				if (pair.Length == 2)
				{
					yield return new KeyValuePair<string, string>(pair[0].Trim(), pair[1].Trim());
				}
			}
		}

		public static string GetTranslation(string key)
		{
			return entries.TryGetValue(key, out string val) ? val : "Unknown";
		}

		public static string GetTranslation(string key, params object[] data)
		{
			return entries.TryGetValue(key, out string val) ? string.Format(val, data) : "Unknown";
		}
	}
}