using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Console
{
    class Program
    {
        static void Main(string[] clArguments)
        {
            bool validArguments = clArguments.Any(a => String.Compare(a, "-json", true) == 0) || clArguments.Any(a => String.Compare(a, "-xml", true) == 0);

            if (validArguments)
            {
                List<IFileHandling> fileHandlings = new List<IFileHandling>() { new ArgumentJson(), new ArgumentXml() };

                List<string> jsonItems = new List<string>();
                List<string> xmlItems = new List<string>();

                foreach(IFileHandling ifh in fileHandlings)
                {
                    if (ifh is ArgumentJson)
                    {
                        jsonItems = ifh.GetParsedData(clArguments).ToList();
                    }
                    else if (ifh is ArgumentXml)
                    {
                        xmlItems = ifh.GetParsedData(clArguments).ToList();
                    }
                }

                List<string> allItems = jsonItems.Concat(xmlItems).OrderBy(i => i).ToList();
                allItems.ForEach(i => Console.WriteLine("{0}", i));
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }

        }
    }
}
