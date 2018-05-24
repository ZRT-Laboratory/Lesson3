using Project_Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            bool validArguments = clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0);

            if (validArguments)
            {
                IFileHandling fileHandling = new ArgumentJson();                

                List<string> allItems = fileHandling.GetParsedData(clArguments).OrderBy(fh => fh).ToList();
                allItems.ForEach(i => Console.WriteLine("{0}", i));
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
