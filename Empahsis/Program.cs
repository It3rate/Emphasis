using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empahsis
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//PronounceParser pp = new PronounceParser();
			//FastTextEmbedding fte = new FastTextEmbedding();
			//StanfordNLP snlp = new StanfordNLP();
			//WikiCleaned wc = new WikiCleaned();
			FrameNet fe = new FrameNet();

			Application.Run(new Form1());
		}
	}
}
