using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrabbleWordSuggester
{
    public class Suggester
    {
        string _query;
        int _top;
        public List<string> Errors { get; set; }

        public Suggester(string query)
        {
            _query = query;
            _top = 10;
            Errors = new List<string>();
        }

        public Suggester(string query, int top)
        {
            _query = query;
            _top = top;
            Errors = new List<string>();
        }

        public Suggester(string query, string top)
        {
            _query = query;
            int.TryParse(top, out _top);
            Errors = new List<string>();
        }

        public bool ValidateInput()
        {
            if (_query.Length == 0)
                Errors.Add("no query string has been passed");
            else if (!Regex.IsMatch(_query, @"^[a-zA-Z]+$")) //must be letters only
                Errors.Add("invalid query string");
            if (_top < 1)
                Errors.Add("invalid top number");
            return Errors.Count > 0 ? false : true;
        }

        public string [] Suggest()
        {
            List<RankWordPair> _words = new List<RankWordPair>();

            foreach (char c in _query.Distinct())
            {
                StreamReader file = new StreamReader(Environment.CurrentDirectory + "\\Indexes\\" + c + ".txt");
                string pairStr;
                while ((pairStr = file.ReadLine()) != null)
                {
                    RankWordPair pair = new RankWordPair(pairStr.Split(' ')[1], int.Parse(pairStr.Split(' ')[0]));
                    _words.Add(pair);
                }
                file.Close();
            }
            string [] _finalWords = _words
                .OrderByDescending(w => w.Rank)
                .Select(w => w.Word)
                .Distinct()
                .Where(w => w.Contains(_query))
                .Take(_top)
                .ToArray();

            return _finalWords;

        }
    }
}
