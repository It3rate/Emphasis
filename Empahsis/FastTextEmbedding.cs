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
        public fixed float values[100000 * 300];
    }

    unsafe class FastTextEmbedding
    {
        private string embeddingLocation = @"D:\store\wiki-news-300d-1M-subword.vec\wiki-news-300d-1M-subword.vec";
        public FastTextValues fastText;
        public List<string> words = new List<string>();

        public FastTextEmbedding()
        {
            File.SetAttributes(embeddingLocation, FileAttributes.Normal);
            using (System.IO.StreamReader sr = new System.IO.StreamReader(embeddingLocation))
            {
                string line;
                char[] sep = { ' ' };
                line = sr.ReadLine(); // 999994 300 first line (entries, encoding size)
                int pageIndex = 0;

                while ((line = sr.ReadLine()) != null&& pageIndex < 100000 * 300)
                {
                    string[] elems = line.Split(sep);
                    words.Add(elems[0]);
                    for (int i = 0; i < 300; i++)
                    {
                        fastText.values[pageIndex + i] = float.Parse(elems[i + 1]);
                    }
                    pageIndex += 300;
                }
            }

            Console.WriteLine(words);
            //MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(embeddingLocation);
            //MemoryMappedViewStream mms = mmf.CreateViewStream();
            //using (TextReader r = new TextReader(mms))
            //{
            //}
            
        }
    }
}
