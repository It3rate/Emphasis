using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Empahsis
{
	class WikiCleaned
	{
		//private string corpusLocation = @"D:\store\corpus\documents_utf8_filtered_20pageviews.csv";
		private string corpusLocation = @"D:\store\corpus\books\cleaned\";
		private string writeLocation = @"D:\store\corpus\books\parses\";
		private string[] bookNames = { "test", "vigilante", "hitchhikers2", "hitchhikers3", "prideAndPred", "sherlockHounds", "warAndPeace" };
		//private string corpusLocation = @"D:\store\corpus\wikitest.csv";
		private int maxSampleSize = 100000;
		private int startIndex = 0;
		public WikiCleaned()
		{
			foreach (string bookName in bookNames)
			{
				parseBook(corpusLocation + bookName + ".txt", bookName);
			}
		}

		void parseBook(string location, string bookName)
		{
			File.SetAttributes(location, FileAttributes.Normal);
			Directory.CreateDirectory(writeLocation + bookName);

			using (System.IO.StreamReader sr = new System.IO.StreamReader(location))
			{
				StanfordNLP snlp = new StanfordNLP();
				//setupSentence();
				snlp.setupDoc();

				string line;
				int index = 0;
				for (int i = 0; i < startIndex; i++)
				{
					sr.ReadLine();
				}
				while ((line = sr.ReadLine()) != null && index < maxSampleSize)
				{
					StreamWriter tw = new StreamWriter(writeLocation + bookName + @"\" + bookName + "_" + index, true);
					tw.WriteLine(bookName + "_" + index);
					tw.WriteLine(line);
					tw.WriteLine();

					//string parse = snlp.parseLines(line);
					string parse = snlp.parseDoc(line);
					tw.WriteLine(parse);
					tw.Flush();
					tw.Close();
					index++;
				}
			}
		}
		void parse(string location)
		{
			File.SetAttributes(location, FileAttributes.Normal);
			Directory.CreateDirectory(writeLocation);

			using (System.IO.StreamReader sr = new System.IO.StreamReader(location))
			{
				StanfordNLP snlp = new StanfordNLP();
				string line;
				int index = 0;
				string[] sep = { ". " };
				// wikipedia-23956711," Return-oriented programming  Return-oriented programming (ROP) is a computer...modified attack. "
				Regex rgx = new Regex(@"^(wikipedia-[0-9]+),"" ((?:[^ ]* )+?) (.*) ""$", RegexOptions.Compiled);
				Match match;

				for (int i = 0; i < startIndex; i++)
				{
					sr.ReadLine();
				}

				while ((line = sr.ReadLine()) != null && index < maxSampleSize)
				{
					match = rgx.Match(line);
					if (match.Success && match.Groups.Count > 2)
					{
						StreamWriter tw = new StreamWriter(writeLocation + match.Groups[1], true);
						tw.WriteLine(match.Groups[1]);
						tw.WriteLine(match.Groups[2]);
						tw.WriteLine();

						string parse = snlp.parseDoc(match.Groups[3].ToString());
						//tw.Write(sentence);
						tw.WriteLine(parse);

						//Console.WriteLine("MATCH VALUE: " + match.Groups[2]);
						//string[] sentences = match.Groups[3].ToString().Split(sep, StringSplitOptions.RemoveEmptyEntries);
						//foreach (string s in sentences)
						//{
						//	string sentence = s + ".";
						//	try
						//	{
						//		string parse = snlp.parse(sentence);
						//		tw.Write(sentence);
						//		tw.WriteLine(parse);
						//	}
						//	catch(Exception e)
						//	{
						//		Console.WriteLine(e.Message);
						//	}
						//}
						tw.Flush();
						tw.Close();
					}
					index++;
				}
			}
		}
	}
}
