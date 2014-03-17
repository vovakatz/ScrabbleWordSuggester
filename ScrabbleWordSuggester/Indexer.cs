using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleWordSuggester
{
    public class Indexer
    {
        List<RankWordPair> _tempList = new List<RankWordPair>(); 

        public void CreateIndex()
        {
            StreamReader file = new StreamReader(Environment.CurrentDirectory + "\\Source\\word_list_moby_crossword.flat.txt");
            Console.WriteLine("starting indexing");

            string word;
            while ((word = file.ReadLine()) != null)
            {
                int rank = CalculateRank(word);
                _tempList.Add(new RankWordPair(word, rank));
            }
            WriteIndexesToDisk();
            Console.WriteLine("finished indexing");

            file.Close();
            // Suspend the screen.
            Console.ReadLine();
        }

        private int CalculateRank(string word)
        {
            int rank = 0;
            foreach (char c in word.ToLower())
            {
                switch (c)
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'l':
                    case 'n':
                    case 'o':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                        rank += 1;
                        break;
                    case 'd':
                    case 'g':
                        rank += 2;
                        break;
                    case 'b':
                    case 'c':
                    case 'm':
                    case 'p':
                        rank += 3;
                        break;
                    case 'f':
                    case 'h':
                    case 'v':
                    case 'w':
                    case 'y':
                        rank += 4;
                        break;
                    case 'k':
                        rank += 5;
                        break;
                    case 'j':
                    case 'x':
                        rank += 8;
                        break;
                    case 'q':
                    case 'z':
                        rank += 10;
                        break;
                    default:
                        break;
                }
            }
            return rank;
        }

        private void WriteIndexesToDisk()
        {
            _tempList = _tempList.OrderByDescending(w => w.Rank).ToList();

            for (char c = 'a'; c <= 'z'; ++c)
            {
                var stringList = string.Join("\n", _tempList.Where(p => p.Word.Contains(c)).Select(w => w.ToString()));
                string filePath = Environment.CurrentDirectory + "\\indexes\\" + c + ".txt";
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(stringList);
                }
            }
        }
    }
}
