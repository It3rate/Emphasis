using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Empahsis
{
	class PronounceParser
	{
		public Dictionary<string, WordData> words = new Dictionary<string, WordData>();
		private string hypenationLocation = "/dicts/EnglishHyphDict_v108.txt";
		private string pronounceLocation = "/dicts/cmudict-0.7b";
		private Dictionary<string, string> hyphenation = new Dictionary<string, string>();
		private Dictionary<string, string[]> pronounce = new Dictionary<string, string[]>();

		public PronounceParser()
		{
			string[] lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + hypenationLocation);
			ParseHypenation(lines);

			lines = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + pronounceLocation);
			ParseCMU(lines);
			var wordData = words.Values;
			IEnumerable<string> wordQuery =
				from word in wordData
				where word.hyphenationCount == 4 
				&& word.syllableEmphasis.Length > 3
				&& word.syllableEmphasis[3] == 2
				select word.text;

			foreach (string w in wordQuery)
			{
				Debug.Write(w + " ");
			}  
		}

		private void ParseHypenation(string[] entries)
		{
			int[] counts = new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (string line in entries)
			{
				string[] segments = line.Remove(line.Length - 1).Split(' ');
				int syllableCount = segments[1].Split('-').Length;
				string wordUpper = segments[0].ToUpper();
				if(syllableCount < 10 && !hyphenation.ContainsKey(wordUpper))
				{
					counts[syllableCount]++;
					hyphenation.Add(wordUpper, segments[1]);
				}
			}
		}
		private void ParseCMU(string[] entries)
		{
			string[] splitter = new string[] { "  " };
			foreach (string line in entries)
			{
				if (line.Length > 2 && !line.StartsWith(";;;"))
				  {
					string[] segments = line.Split(splitter, StringSplitOptions.None);
					Lexeme[] lexemes = ParsePronounce(segments[1]);
					string wordUpper = segments[0].ToUpper();
					WordData wordData;
					if (hyphenation.ContainsKey(wordUpper))
					{
						string hyphenationString = hyphenation[segments[0]];
						string[] syllables = hyphenationString.ToUpper().Split('-');
						wordData = new WordData(wordUpper, lexemes, syllables);
					}
					else
					{
						wordData = new WordData(wordUpper, lexemes,  Array.Empty<string>());
						//Console.WriteLine(line);
					}
					words.Add(wordUpper, wordData);
				}
			}
		}

		private Lexeme[] ParsePronounce(string pronounce)
		{
			string[] segments = pronounce.Split(' ');
			Lexeme[] result = new Lexeme[segments.Length];
			for(int i = 0; i < segments.Length; i++)
			{
				result[i] = WordData.terminals[segments[i]];
			}
			return result;
		}
	}

}
