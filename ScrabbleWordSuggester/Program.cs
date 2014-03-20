using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleWordSuggester
{
    class Program
    {
        static void Main(string[] args)
        {
            bool _quit = false;

            while (!_quit)
            {
                Console.Write("enter command: ");
                string line;
                while ((line = Console.ReadLine().Trim()) == "") ;
                args = line.Split(' ');

                switch (args[0].ToLower())
                {
                    case "scrabble-indexer":
                        Indexer indexer;
                        if (args.Length == 1)
                            indexer = new Indexer();
                        else
                            indexer = new Indexer(string.Join(" ", args, 1, args.Count() - 1));
                        if (indexer.ValidateInput())
                            indexer.CreateIndex();
                        else
                            OutputHelper.PrintToConsole(indexer.Errors);
                        break;
                    case "scrabble-suggester":
                        Suggester suggester = new Suggester(args[1], args[2]);
                        if (suggester.ValidateInput())
                        {
                            string[] suggestedWords = suggester.Suggest();
                            OutputHelper.PrintToConsole(suggestedWords);
                        }
                        break;
                    case "Q":
                        _quit = true;
                        break;
                    default:
                        Console.WriteLine("you entered unrecognized command");
                        break;
                }

            }
        }
    }
}
