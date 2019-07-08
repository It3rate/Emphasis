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
            FastTextEmbedding fte = new FastTextEmbedding();

            Application.Run(new Form1());
        }
    }
}
