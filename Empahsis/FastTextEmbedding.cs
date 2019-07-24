using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empahsis
{
	internal unsafe struct FastTextValues
	{
		public fixed float values[FastTextEmbedding.entries * 300];
	}

	unsafe class FastTextEmbedding
	{
		public const int entries = 100000;
		public int embedSize = 300;
		private string embeddingLocation = @"D:\store\wiki-news-300d-1M-subword.vec";
		public FastTextValues fastText;
		public Dictionary<string, int> words = new Dictionary<string, int>();

		// various sorted test lists
		readonly string[] bodyNouns = { "atom", "speck", "pebble", "finger", "hand", "arm", "body", "room", "road", "earth", "galaxy", "universe" };
		readonly string[] sizedNouns = { "strawberry", "bean", "egg", "tomato", "melon", "watermelon", "log", "box", "suitcase", "sheep", "cow", "car", "truck", "house" };


		public FastTextEmbedding()
		{
			File.SetAttributes(embeddingLocation, FileAttributes.Normal);
			using (System.IO.StreamReader sr = new System.IO.StreamReader(embeddingLocation))
			{
				string line;
				char[] sep = { ' ' };
				line = sr.ReadLine(); // 999994 300 first line (entries, encoding size)
				string[] metrics = line.Split(sep);
				//entries = int.Parse(metrics[0]);
				embedSize = int.Parse(metrics[1]);

				int pageIndex = 0;

				while ((line = sr.ReadLine()) != null&& pageIndex < entries * embedSize)
				{
					string[] elems = line.Split(sep);
					words.Add(elems[0], pageIndex / embedSize);
					for (int i = 0; i < embedSize; i++)
					{
						fastText.values[pageIndex + i] = float.Parse(elems[i + 1]);
					}
					pageIndex += embedSize;
				}
			}

			//Dictionary<string, double> results = GetSizedNounsBySizedVerbs();
			//Dictionary<string, double> results = GetSizedNounsByBodyNouns();
			Dictionary<string, double> results = GetClosestWordsToBodyNouns(bodyNouns);
			Console.WriteLine(results);

		}

		Dictionary<string, double> GetClosestWordsToBodyNouns(string[] wordList)
		{
			var results = new Dictionary<string, double>();
			double cutoff = 0.25;
			foreach (string s in wordList)
			{
				if (words.ContainsKey(s))
				{
					int w0 = words[s];
					var temp = new Dictionary<string, double>();
					foreach (string key in words.Keys)
					{
						int index = words[key];
						if(index > 50)
						{
							double val = GetCosineSimilarity(w0, index);
							if (val > cutoff)
							{
								temp.Add(s + "," + key, val);
							}
						}
					}
					var list = temp.ToList();
					list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
					int count = 20;
					foreach(var kv in list)
					{
						results.Add(kv.Key, kv.Value);
						count--;
						if(count <= 0)
						{
							break;
						}
					}
				}
			}
			return results;
		}

		Dictionary<string, double> GetSizedNounsByBodyNouns()
		{
			Dictionary<string, double> results = new Dictionary<string, double>();
			foreach (string s in sizedNouns)
			{
				if (words.ContainsKey(s))
				{
					int w0 = words[s];
					foreach (string verb in bodyNouns)
					{
						if (words.ContainsKey(verb))
						{
							int w1 = words[verb];
							double val = GetCosineSimilarity(w0, w1);
							results.Add(s + "," + verb, val);
						}
					}
				}
			}
			return results;
		}

		Dictionary<string, double> GetSizedNounsBySizedVerbs()
		{
			string[] fingerVerbs = { "pinch", "scratch", "itch", "pick", "twiddle", "examine", "nudge", "focus", "tweak" };
			string[] handVerbs = { "clasp", "clutch", "grasp", "grip", "handle", "hold", "wield", "grab", "rub", "loosen", "punch" };
			string[] armVerbs = { "carry", "drag", "haul", "heave", "hoist", "kick", "lug", "pull", "push", "shove", "tow", "tug" };

			Dictionary<string, double> results = new Dictionary<string, double>();

			foreach (string s in sizedNouns)
			{
				if (words.ContainsKey(s))
				{
					int w0 = words[s];
					foreach (string verb in fingerVerbs)
					{
						if (words.ContainsKey(verb))
						{
							int w1 = words[verb];
							double val = GetCosineSimilarity(w0, w1);
							results.Add(s + "," + verb, val);
						}
					}
					foreach (string verb in handVerbs)
					{
						if (words.ContainsKey(verb))
						{
							int w1 = words[verb];
							double val = GetCosineSimilarity(w0, w1);
							results.Add(s + "," + verb, val);
						}
					}
					foreach (string verb in armVerbs)
					{
						if (words.ContainsKey(verb))
						{
							int w1 = words[verb];
							double val = GetCosineSimilarity(w0, w1);
							results.Add(s + "," + verb, val);
						}
					}
				}
			}
			return results;
		}

		double GetCosineSimilarity(int word0, int word1)
		{
			double dot = 0.0d;
			double mag0 = 0.0d;
			double mag1 = 0.0d;
			int wi0 = word0 * embedSize;
			int wi1 = word1 * embedSize;
			for (int n = 0; n < embedSize; n++)
			{
				dot += fastText.values[wi0 + n] * fastText.values[wi1 + n];
				mag0 += Math.Pow(fastText.values[wi0 + n], 2);
				mag1 += Math.Pow(fastText.values[wi1 + n], 2);
			}

			return dot / (Math.Sqrt(mag0) * Math.Sqrt(mag1));
		}
	}
}
