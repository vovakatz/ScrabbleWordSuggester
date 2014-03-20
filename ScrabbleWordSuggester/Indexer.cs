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
        string _sourceFilePath;
        public List<string> Errors{ get; set; }

        public Indexer()
        {
            _sourceFilePath = Environment.CurrentDirectory + "\\Source\\word_list_moby_crossword.flat.txt";
            Errors = new List<string>();
        }

        public Indexer(string sourceFilePath)
        {
            _sourceFilePath = sourceFilePath;
            Errors = new List<string>();
        }

        public bool ValidateInput()
        {
            if (!File.Exists(_sourceFilePath))
                Errors.Add("specified file does not exist");
            return Errors.Count > 0 ? false : true;
        }

        public void CreateIndex()
        {
            Console.WriteLine("starting indexing");
            StreamReader file = new StreamReader(_sourceFilePath);

            string word;
            while ((word = file.ReadLine()) != null)
            {
                int rank = CalculateRank(word);
                _tempList.Add(new RankWordPair(word, rank));
            }
            WriteIndexesToDisk();
            Console.WriteLine("finished indexing");

            file.Close();
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

            string[] letters = "a b c d e f g h i j k l m n o p q r s t u v w x y z".Split(' ');
            Parallel.ForEach(letters, currentLetter =>
            {
                var stringList = string.Join("\n", _tempList.Where(p => p.Word.Contains(currentLetter)).Select(w => w.ToString()));
                string filePath = Environment.CurrentDirectory + "\\indexes\\" + currentLetter + ".txt";
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(stringList);
                }
            });
        }
    }
}
