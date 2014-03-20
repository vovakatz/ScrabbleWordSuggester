using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleWordSuggester
{
    public class OutputHelper
    {
        public static void PrintToConsole(List<string> items)
        {
            foreach (string item in items)
                Console.WriteLine(item);
        }

        public static void PrintToConsole(string [] items)
        {
            foreach (string item in items)
                Console.WriteLine(item);
        }
    }
}
