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
        public void CreateIndex()
        {
            string line;
            StreamReader file = new StreamReader(Environment.CurrentDirectory + "\\Source\\word_list_moby_crossword.flat.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.");
            // Suspend the screen.
            System.Console.ReadLine();
        }

        private void CalculateRank(string word)
        {
            foreach (char c in word.ToLower())
            {
                switch (c)
                    case: 'a'
            }
        }
    }
}
