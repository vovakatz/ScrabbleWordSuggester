using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleWordSuggester
{
    public class RankWordPair
    {
        public string Word { get; set; }
        public int Rank { get; set; }

        public RankWordPair(string word, int rank)
        {
            this.Word = word;
            this.Rank = rank;
        }

        public override string ToString()
        {
            return Rank + " " + Word;
        }
    }
}
