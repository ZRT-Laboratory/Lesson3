using Project_Xml;
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
                IFileHandling ifhXml =  new ArgumentXml();

                List<string> allItems = ifhXml.GetParsedData(clArguments).OrderBy(ifh => ifh).ToList();
                allItems.ForEach(i => Console.WriteLine("{0}", i));
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
